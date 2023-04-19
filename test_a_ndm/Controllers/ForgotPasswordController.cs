using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test_A_NDM.Models;
using Test_A_NDM.Services;

namespace Test_A_NDM.Controllers
{
    public class ForgotPasswordController : ApiController
    {
        private readonly IForgotPasswordService _forgotPasswordService;

        public ForgotPasswordController(IForgotPasswordService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost]
        [Route("~/api/forgotpassword/reset")]
        public IHttpActionResult ResetPassword(ForgotPassword model)
        {
            if (model == null)
            {
                return BadRequest("No data found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isSuccess = _forgotPasswordService.ResetPassword(model);

            if (!isSuccess)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}