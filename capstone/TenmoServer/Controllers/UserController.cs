using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserDao dao;

        string connectionString = "Server=.\\SQLEXPRESS;Database=tenmo;Trusted_Connection=True;";

        public UserController()
        {
            dao = new UserSqlDao(connectionString);
        }
        [HttpGet]
        public IList<User> GetUsers()
        {
            string userName = User.Identity.Name;
            return dao.GetUsers(userName);
        }
    }
}
