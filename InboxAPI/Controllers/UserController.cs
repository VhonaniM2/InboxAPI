using System.Web.Http;
using InboxAPI.Interfaces;
using InboxAPI.Repositories;
using InboxAPI.Models;
using System.Net;

namespace InboxAPI.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private readonly IDataLayer _dataLayer = new DataLayer();

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userName">email address</param>
        /// <param name="password">Password</param>
        /// <param name="fullnames">fullNames</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public OperationOutcome RegisterUser(string userName, string password, string fullnames)
        {
            try
            {
                var result = _dataLayer.RegisterNewUser(userName, Helper.GetHash(password), fullnames);

                if (!result.IsSuccessful)
                {
                    return new OperationOutcome() { IsSuccessful = result.IsSuccessful, Error = result.Message };
                }

                return new OperationOutcome() { IsSuccessful = true };
            }
            catch
            {
                return new OperationOutcome() { Error = HttpStatusCode.InternalServerError.ToString(), IsSuccessful = false };
            }
        }
    }
}
