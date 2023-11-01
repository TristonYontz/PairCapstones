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
    }
}
