using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        public IList<Transfer> GetAllTransfer(Account account);
    }
}
