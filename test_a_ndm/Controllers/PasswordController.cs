using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_a_Ndm.Data;
using Test_a_Ndm.Models;

namespace Test_a_Ndm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly TestContext _context;

        public PasswordController(TestContext context)
        {
            _context = context;
        }

        // POST: api/Password
        [HttpPost]
        public IActionResult PostPassword([FromBody] Password password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Reset Password
            _context.Passwords.Add(password);
            _context.SaveChanges();

            return CreatedAtAction("GetPassword", new { id = password.PasswordId }, password);
        }

        // GET: api/Password/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPassword([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get Password
            var password = await _context.Passwords.FindAsync(id);

            if (password == null)
            {
                return NotFound();
            }

            return Ok(password);
        }

        // PUT: api/Password/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassword([FromRoute] int id, [FromBody] Password password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != password.PasswordId)
            {
                return BadRequest();
            }

            // Update Password
            _context.Entry(password).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PasswordExists(int id)
        {
            return _context.Passwords.Any(e => e.PasswordId == id);
        }
    }
}