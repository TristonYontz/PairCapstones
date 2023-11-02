using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountDao dao;
        string connectionString = "Server=.\\SQLEXPRESS;Database=tenmo;Trusted_Connection=True;";
        public AccountController()
        {
            dao = new AccountDao(connectionString);
        }
        [HttpGet]
        public ActionResult<Account> GetAccount()
        {

            string userName = User.Identity.Name;
            return dao.GetAccount(userName);

        }

    }
}
