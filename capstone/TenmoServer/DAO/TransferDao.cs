using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Principal;
using TenmoServer.Exceptions;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferDao : ITransferDao
    {
        public TransferDao(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private string connectionString = "";

        string sqlGetAllTransfer = "SELECT transfer_id, transfer_type_id, transfer_status_id, amount, " +
                                   "aFrom.account_id as 'fromAcct', aFrom.user_id as 'fromUser', uFrom.username as 'fromName', aFrom.balance as 'fromBal', " +
                                   "aTo.account_id as 'toAcct', aTo.user_id as 'toUser', uTo.username as 'toName', aTo.balance as 'toBal' " +
                                   "FROM transfer " +
                                   "INNER JOIN account aFrom on account_from = aFrom.account_id " +
                                   "INNER JOIN tenmo_user uFrom on uFrom.user_id = aFrom.user_id " +
                                   "INNER JOIN account aTo on account_to = aTo.account_id " +
                                   "INNER JOIN tenmo_user uTo on uTo.user_id = aTo.user_id " +
                                   "WHERE (account_from IN (SELECT account_id FROM account WHERE user_id = @user_id) OR " +
                                   "account_to IN(SELECT account_id FROM account WHERE user_id = @user_id)) ";




        public IList<Transfer> GetAllTransfer(Account account)
        {
            IList<Transfer> transfers = new List<Transfer>();


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlGetAllTransfer, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", account.UserId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        

                        while (reader.Read())
                        {
                            Transfer transfer = MapRowToTransfer(reader);
                            transfers.Add(transfer);
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return transfers;
        }
        private Transfer MapRowToTransfer(SqlDataReader reader)
        {
            Transfer transfer = new Transfer();
            transfer.AccountFromName = Convert.ToString(reader["toName"]);
            transfer.AccountToName = Convert.ToString(reader["fromName"]);
            transfer.Amount = Convert.ToDecimal(reader["amount"]);
            transfer.TransferSatusId = Convert.ToInt32(reader["transfer_status_id"]);
            transfer.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
            return transfer;
        }
    }
}
