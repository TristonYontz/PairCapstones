using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountDao : IAccountDao
    {
        public AccountDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string connectionString = "";
        public Account GetAccount(string userName)
        {
            Account account = new Account();

            string sql = "SELECT balance , account_id , account.user_id " +
                    "FROM account " +
                    "JOIN tenmo_user ON tenmo_user.user_id = account.user_id " + 
                    "WHERE username = @username ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
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
            return account;
        }
        public Account GetAccountByUserId(int userId)
        {
            Account account = new Account();

            string sql = "SELECT balance , account_id , account.user_id " +
                    "FROM account " +
                    "WHERE user_id = @user_id ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
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
            return account;
        }
        public Account UpdateAccount(Account updatedAccount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string SqlUpdateAccount = "UPDATE account SET balance=@balance " +
                "WHERE account.user_id = @id ";

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
    }
}
