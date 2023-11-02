using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        public Account GetAccount(string usernName);
        public Account UpdateAccount(Account updatedAccount);
        public Account GetAccountByUserId(int UserId);
    }
}
