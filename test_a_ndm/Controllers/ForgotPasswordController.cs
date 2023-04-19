using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using test_a_ndm.Models;

namespace test_a_ndm.Controllers
{
    public class ForgotPasswordController : ApiController
    {
        [HttpPost]
        [Route("forgotpassword")]
        public async Task<HttpResponseMessage> ForgotPassword(User user)
        {
            try
            {
                //Check if user exists
                if(UserExists(user.Email))
                {
                    // Generate Password Reset Token
                    string token = GeneratePasswordResetToken();
                    // Set Reset Token in DB
                    await SetPasswordResetTokenInDB(user.Email, token);
                    // Send Email with Password Reset Link
                    await SendPasswordResetLink(user.Email, token);
                    return Request.CreateResponse(HttpStatusCode.OK, "Password Reset Link sent to your email");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "User doesn't exists");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<HttpResponseMessage> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                // Verify Password Reset Token
                if (await VerifyPasswordResetToken(resetPassword.Email, resetPassword.Token))
                {
                    // Set New Password
                    await SetNewPassword(resetPassword.Email, resetPassword.NewPassword);
                    return Request.CreateResponse(HttpStatusCode.OK, "Password Updated Successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Token");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Check if user exists
        public bool UserExists(string email)
        {
            // Code to check if user exists
            return true;
        }

        // Generate Password Reset Token 
        public string GeneratePasswordResetToken()
        {
            // Code to generate random string for token
            return "Token123";
        }

        // Set Password Reset Token in DB
        public async Task<bool> SetPasswordResetTokenInDB(string email, string token)
        {
            // Code to save token in DB
            return true;
        }

        // Send Email with Password Reset Link
        public async Task SendPasswordResetLink(string email, string token)
        {
            // Code to send password reset link
        }

        // Verify Password Reset Token
        public async Task<bool> VerifyPasswordResetToken(string email, string token)
        {
            // Code to verify token
            return true;
        }

        // Set new Password
        public async Task<bool> SetNewPassword(string email, string newPassword)
        {
            // Code to set new password
            return true;
        }
    }
}