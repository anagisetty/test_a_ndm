using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using test_a_ndm.Services;

namespace test_a_ndm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public ForgotPasswordController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// Generates forgot password token and sends it to the given email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerateForgotPasswordToken")]
        public async Task<IActionResult> GenerateForgotPasswordToken(string email)
        {
            try
            {
                var resetToken = await _userService.GenerateForgotPasswordToken(email);
                if (string.IsNullOrEmpty(resetToken))
                    return NotFound("No user found with the given email address.");

                //Send forgot password reset token email
                await _userService.SendForgotPasswordResetTokenEmail(email, resetToken);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Resets the user's password with the given token and new password
        /// </summary>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string token, string password)
        {
            try
            {
                var resetSuccessful = await _userService.ResetPassword(token, password);
                if (!resetSuccessful)
                    return BadRequest("Failed to reset password.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}