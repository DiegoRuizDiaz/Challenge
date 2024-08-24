using ApiDevBP.Services.Interfaces;
using ApiDevBP.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _iUserService;

        public UsersController(ILogger<UsersController> logger, IUserService iUserService)
        {
            _logger = logger;
            _iUserService = iUserService;
        }

        /// <summary>
        /// Returns a list of users.
        /// </summary>
        /// <response code="200">Returns a list of users.</response>
        /// <response code="404">No users found.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _iUserService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// This method provides the ID of a user(s) so that it can be easily accessed
        /// </summary>
        /// <response code="200">Returns the ID of the users found</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpGet("{name}/{lastName}")]
        public async Task<IActionResult> GetUserId([FromRoute] string name, string lastName)
        {
            try
            {
                var usersWithId = await _iUserService.Get(name, lastName);
                return Ok(usersWithId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <response code="200">User created successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            try
            {
                await _iUserService.AddUser(user);
                return Ok();                
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Method : Add user , has failed with an unexpected exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Modify an existing user using their ID.
        /// </summary>
        /// <response code="200">User successfully modified.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No user found.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserModel user)
        {
            try
            {
                await _iUserService.UpdateUser(id, user);
                return Ok();
            }           
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Method : Update user , has failed with an unexpected exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an existing user using their ID.
        /// </summary>
        /// <response code="200">User successfully deleted.</response>
        /// <response code="404">No user found.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                await _iUserService.DeleteUser(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {                
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {           
                _logger.LogInformation("Method : Delete user , has failed with an unexpected exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
