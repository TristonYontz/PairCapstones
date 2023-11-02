using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Exceptions;
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
        [HttpGet("{userId}")]
        public ActionResult<Account> GetAccountByUserId(int userId)
        {
            Account account = dao.GetAccountByUserId(userId);
            if (account != null)
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Account> UpdateAccount(int id, Account account)
        {
            // The id on the URL takes precedence over the one in the payload, if any
            account.Id = id;

            try
            {
                Account updateAccount = dao.UpdateAccount(account);
                return Ok(updateAccount);

            }
            catch (DaoException)
            {
                return NotFound();
            }
        }

    }
}
