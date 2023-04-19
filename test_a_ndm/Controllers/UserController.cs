using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test_a_NDM.Models;

namespace Test_a_NDM
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public HttpResponseMessage ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            bool isSuccess = _repository.ForgotPassword(model);

            if (isSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occurred");
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public HttpResponseMessage ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            bool isSuccess = _repository.ResetPassword(model);

            if (isSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occurred");
            }
        }
    }
}