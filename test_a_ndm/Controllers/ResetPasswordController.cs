using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using test_a_ndm.Data;

namespace test_a_ndm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _dbContext;

        public ResetPasswordController(IConfiguration config, ApplicationDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult ResetPassword([FromBody]ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //validate email
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                return NotFound("User not found!");
            }

            //generate password reset code
            var resetCode = Guid.NewGuid().ToString();
            user.PasswordResetCode = resetCode;
            _dbContext.SaveChanges();

            //send reset code in email
            //to do

            return Ok("Password reset code has been sent to your email!");
        }
    }

    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
}