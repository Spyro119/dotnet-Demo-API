using Demo_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace Demo_API.Controllers {

  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  // [Authorize]
  public class ApplicationUserController : ControllerBase {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthorizationService _authorizationService;

     public ApplicationUserController( UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IAuthorizationService authorizationService) {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Route("")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetAllUsers() {

      IEnumerable<ApplicationUser> userList;
      List<Object> formattedUserList = new List<Object>();

      userList = await _userManager.Users.ToListAsync();

      foreach(ApplicationUser user  in userList) {
        var role = await _userManager.GetRolesAsync(user);
        formattedUserList.Add(new {
          Id = user.Id,
          FirstName = user.FirstName,
          LastName = user.LastName,
          UserName = user.UserName,
          Role = role,
          Email = user.Email,
          EmailConfirmed = user.EmailConfirmed,
          FailedLoginCount = user.AccessFailedCount,
          PhoneNumber = user.PhoneNumber,
          PhoneNumberConfirmed = user.PhoneNumberConfirmed,
          AccountLocked = user.LockoutEnabled,
          LockOutEnd = user.LockoutEnd
        });
      }
      
      return Ok( formattedUserList );
    }
 
    [HttpPut]
    [Route("edit/{username}")]
    public async Task<IActionResult> EditProfile(string username, [FromBody]ApplicationUser model) {
      if ( model is null ) return BadRequest("No User was provided.");
      var updatedUser = await _userManager.FindByNameAsync(username);
      if (updatedUser is null) return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"Failed to update Account {model.Id}." });

      if ( User.Identity?.Name != updatedUser.UserName ) return Unauthorized("You are not allowed to edit this profile");
      
      updatedUser.FirstName = model.FirstName;
      updatedUser.LastName = model.LastName;
      updatedUser.UserName = model.Email;
      updatedUser.Email = model.Email;
      updatedUser.PhoneNumber = model.PhoneNumber;

      var result = await _userManager.UpdateAsync(updatedUser);
      if ( !result.Succeeded ) return  StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = result.Errors.First().ToString() });
      return NoContent();
    }
  }
}
