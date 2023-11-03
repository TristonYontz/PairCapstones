using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        ITransferDao dao;
        IAccountDao daoAccount;
        string connectionString = "Server=.\\SQLEXPRESS;Database=tenmo;Trusted_Connection=True;";
        public TransferController()
        {
            dao = new TransferDao(connectionString);
            daoAccount = new AccountDao(connectionString);
        }
        [HttpGet]
        public IList<Transfer> GetAllTransfer()
        {
            string userName = User.Identity.Name;
            Account account = daoAccount.GetAccount(userName);
            return dao.GetAllTransfer(account);
        }
    }
}
