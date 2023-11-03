using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Policy;
using TenmoServer.Exceptions;
using TenmoServer.Models;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public class AccountDao : IAccountDao
    {
        public AccountDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string SqlUpdateAccount = "UPDATE account SET balance = @balance " +
                "WHERE account.user_id = @id ";

        private string SqlGetAccountByUserId = "SELECT balance , account_id , account.user_id " +
                  "FROM account " +
                  "WHERE user_id = @user_id ";

        private string SqlGetAccount = "SELECT balance , account_id , account.user_id " +
                   "FROM account " +
                   "JOIN tenmo_user ON tenmo_user.user_id = account.user_id " +
                   "WHERE username = @username ";

        string SqlCreateTransferRequest = "INSERT INTO transfer (transfer_type_id, transfer_status_id, account_from, account_to, amount ) " +
                        "OUTPUT INSERTED.transfer_id " +
                        "VALUES (@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount)";

        private string connectionString = "";


        public Account GetAccount(string userName)
        {
            Account account = new Account();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();


                    using (SqlCommand cmd = new SqlCommand(SqlGetAccount, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", userName);

                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {
                            if (reader.Read())
                            {
                                account.Balance = Convert.ToDecimal(reader["balance"]);
                                account.Id = Convert.ToInt32(reader["account_id"]);
                                account.UserId = Convert.ToInt32(reader["user_id"]);
                            }
                        }
                    }
                }
            }
            catch(SqlException )
            {
                return null;
            }

 
            return account;
        }
        public Account GetAccountByUserId(int userId)
        {
            Account account = new Account();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(SqlGetAccountByUserId, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                account.Balance = Convert.ToDecimal(reader["balance"]);
                                account.Id = Convert.ToInt32(reader["account_id"]);
                                account.UserId = Convert.ToInt32(reader["user_id"]);
                            }
                        }
                    }
                }
            }
            catch (SqlException )
            {
                return null;
            }
            return account;
        }

        public Account UpdateAccount(Account updatedAccount)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SqlUpdateAccount, conn))
                {
                    cmd.Parameters.AddWithValue("@balance", updatedAccount.Balance);
                    cmd.Parameters.AddWithValue("@id", updatedAccount.UserId);

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return updatedAccount;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        public void  CreateTransferRequest(TransferRequest transferRequest, Account accountTo, Account accountFrom)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();

                // create user
                using (SqlCommand cmd = new SqlCommand(SqlCreateTransferRequest, conn))
                {
                    cmd.Parameters.AddWithValue("@transfer_type_id", 2);
                    cmd.Parameters.AddWithValue("@transfer_status_id",2  );
                    cmd.Parameters.AddWithValue("@amount", transferRequest.Amount);
                    cmd.Parameters.AddWithValue("@account_to", accountTo.Id);
                    cmd.Parameters.AddWithValue("@account_from", accountFrom.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string MakeTransfer(TransferRequest transferRequest)
        {
            Account accountTo = GetAccountByUserId(transferRequest.ToId);
            Account accountFrom = GetAccountByUserId(transferRequest.FromId);

            if (accountFrom.Balance > transferRequest.Amount)
            {
                accountTo.Balance += transferRequest.Amount;
                accountFrom.Balance -= transferRequest.Amount;

                CreateTransferRequest(transferRequest, accountTo, accountFrom);

                UpdateAccount(accountTo);
                UpdateAccount(accountFrom);

                return "Approved";
            }
            else
            {
                return "Insufficient Funds!";
            }
        }
    }
}
