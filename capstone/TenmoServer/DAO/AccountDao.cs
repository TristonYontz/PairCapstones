using System;
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
        public Account GetAccountById(int userId)
        {
            Account account = new Account();

            string sql = "SELECT balance, account_id, user_id FROM account WHERE user_id = @user_id";

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
    }
}
