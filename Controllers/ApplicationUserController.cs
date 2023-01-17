using Demo_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo_API.Data;

namespace Demo_API.Controllers 
{

  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  [Authorize]
  public class ApplicationUserController : ControllerBase 
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthorizationService _authorizationService;
    private readonly ApplicationDbContext _context;

     public ApplicationUserController( 
      UserManager<ApplicationUser> userManager, 
      RoleManager<IdentityRole> roleManager, 
      IConfiguration configuration, 
      IAuthorizationService authorizationService, 
      ApplicationDbContext context) {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _authorizationService = authorizationService;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetAllUsers() 
    {

      IEnumerable<ApplicationUser> userList;
      List<Object> formattedUserList = new List<Object>();
      List<Object> formattedAddressList = new List<Object>();
      List<Object> formattedPetList = new List<Object>();
      List<Object> formattedAppointmentList = new List<Object>();

      userList = await _userManager.Users.ToListAsync();

      foreach(ApplicationUser user  in userList) {
        var role = await _userManager.GetRolesAsync(user);
        var addresses = await _context.Addresses.Where(a => a.UserId == user.Id).ToListAsync();
        var pets = await _context.Pets.Where(p => p.OwnerId == user.Id ).ToListAsync();
        var appointments = await _context.Appointments.Where( ap => ap.ClientId == user.Id ).ToListAsync();
        
        foreach( Address address in addresses )
        {
          formattedAddressList.Add(formatAddressList(address));
        }
        foreach(Appointment appointment in appointments ) 
        {
          formattedAppointmentList.Add(formatAppointmentList(appointment));
        }
        foreach (Pet pet in pets) 
        {
          formattedPetList.Add(formatPetList(pet));
        }
        
        formattedUserList.Add(formatUserList(
          user, 
          role, 
          formattedAddressList, 
          formattedPetList,
          formattedAppointmentList)
        );
      }
      
      return Ok( formattedUserList );
    }
 
    [HttpPut]
    [Route("edit/{username}")]
    public async Task<IActionResult> EditProfile(string username, [FromBody]ApplicationUser model) 
    {
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
      return Ok("Account successfully updated!");
    }

    [HttpPatch]
    [Route("edit-password/{username}")]
    public async Task<IActionResult> ChangePassword(string username, [FromBody] ChangePassword model)
    {
      if (model.NewPassword is null || model.OldPassword is null) return BadRequest("Please enter your New password as well as your Old password.");
      var user = await _userManager.FindByNameAsync(username);
      if (user is null) return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"Failed to Change password. Please try again later." });
      
      if ( User.Identity?.Name != username ) return Unauthorized("You are not allowed to edit this profile");
      
      if (!await _userManager.CheckPasswordAsync(user, model.OldPassword) ) return BadRequest("Current password is incorrect.");
      var response = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

      if (!response.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to change password. Please try again." });
      
      return Ok("Successfully changed your password.");
    }

    [HttpDelete]
    [Route("delete/{username}")]
    public async Task<IActionResult> DeleteUser(string username)
    {
      var user = await _userManager.FindByNameAsync(username);
      if (user is null) return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"Failed to delete Account {username}. Account does not exist." });

      if ( User.Identity?.Name != user.UserName ) return Unauthorized("You are not allowed to delete this account");
      
      var addresses = await _context.Addresses.Where(a => a.UserId == user.Id).ToListAsync();
      var pets = await _context.Pets.Where(p => p.OwnerId == user.Id ).ToListAsync();
      var appointments = await _context.Appointments.Where( ap => ap.ClientId == user.Id ).ToListAsync();
      _context.RemoveRange(appointments);
      _context.RemoveRange(pets);
      _context.RemoveRange(addresses);
      await _context.SaveChangesAsync();

      var result = await _userManager.DeleteAsync(user);
      if ( !result.Succeeded ) return  StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = result.Errors.First().ToString() });
      
      return NoContent();
    }
  
    private Object formatAddressList(Address adr)
    {
      return new {
        Id = adr.Id,
        Country = adr.Country,
        Province = adr.Province,
        City = adr.City,
        Street = adr.Street,
        CivicNumber = adr.CivicNumber,
        PostalCode = adr.PostalCode
      };
    }

    private Object formatUserList(
      ApplicationUser user, 
      IList<string> role, 
      Object addresses,
      Object pets,
      Object appointments) 
      {
      return new {
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
          LockOutEnd = user.LockoutEnd,
          Pets = pets,
          Addresses =  addresses,
          Appointments = appointments
        };
    }
    
    private Object formatPetList(Pet pet)
    {
      return new { 
        Id = pet.Id,
        Breed = pet.Breed,
        Age = pet.Age,
        Birthday = pet.BirthDate,
        AdditionalNotes = pet.AdditionnalNotes
      };
    }
    private Object formatAppointmentList(Appointment appointment) {
      return new {
        Id = appointment.Id,
        StartDate = appointment.StartDate,
        EndDate = appointment.EndDate
      };
    }
  }
}
