using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new List<User>();
        private static int NextId = 1;

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            User? user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { error = "User not found." });
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Create(User newUser)
        {
            string? validationError = ValidateUser(newUser);
            if (validationError != null)
            {
                return BadRequest(new { error = validationError });
            }

            newUser.Id = NextId;
            NextId++;

            Users.Add(newUser);

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public ActionResult<User> Update(int id, User updatedUser)
        {
            string? validationError = ValidateUser(updatedUser);
            if (validationError != null)
            {
                return BadRequest(new { error = validationError });
            }

            User? existingUser = Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(new { error = "User not found." });
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User? user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { error = "User not found." });
            }

            Users.Remove(user);
            return NoContent();
        }

        private string? ValidateUser(User user)
        {
            if (user == null)
            {
                return "User body is required.";
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                return "Name is required.";
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return "Email is required.";
            }

            if (!user.Email.Contains("@") || !user.Email.Contains("."))
            {
                return "Email must be a valid email address.";
            }

            return null;
        }
    }
}
