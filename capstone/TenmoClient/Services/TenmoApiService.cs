using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoServer.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...
        public Account GetAccount()
        {
            RestRequest request = new RestRequest("account");

            IRestResponse<Account> response = client.Get<Account>(request);

            CheckForError(response);

            return response.Data;
        }
        public List<ApiUser> GetUsers()
        {
            RestRequest request = new RestRequest("user");

            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);

            CheckForError(response);

            return response.Data;
        }
        //public ApiUser GetUserById(int userId)
        //{
        //    RestRequest request = new RestRequest("user/" + UserId);

        //    IRestResponse<ApiUser> response = client.Get<ApiUser>(request);

        //    CheckForError(response);

        //    return response.Data;
        //}
    }
}
