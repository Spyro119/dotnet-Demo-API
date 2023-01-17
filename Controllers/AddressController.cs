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
  // [Authorize]
  public class AddressController : ControllerBase 
  {
    private readonly IAuthorizationService _authorizationService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

     public AddressController( IAuthorizationService authorizationService,  ApplicationDbContext context, UserManager<ApplicationUser> userManager ) {
       
        _authorizationService = authorizationService;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetAllAddress() 
    {
      var addresses = await _context.Addresses.ToListAsync();
      return Ok(addresses);
    }
 
    [HttpPut]
    [Route("edit/{username}/{id}")]
    public async Task<IActionResult> EditAddress(string username, [FromBody]Address model) 
    {
      if (model is null) return BadRequest("No Address was provided.");
      var currentUser = await _userManager.FindByNameAsync(username);
      var address = await _context.Addresses.Where(a => a.Id == model.Id ).FirstOrDefaultAsync<Address>();
      if (address is null) return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"Failed to update User's {username} Address." });
      if (User.Identity?.Name != currentUser?.UserName) return Unauthorized("You are not allowed to edit this profile");

      address = model;
      var Response = await _context.SaveChangesAsync();
      // if (Response == 0 )
      return NoContent();
    }

    [HttpPut]
    [Route("{username}/address")]
    public async Task<IActionResult> EditUserAddress(string username, [FromBody] Address model ) 
    {
      if ( model is null ) return BadRequest("No User was provided.");
      var user = await _userManager.FindByNameAsync(username);
      if (user is null) return BadRequest($"Unable to find user {username}. Cannot update Address of not existing user.");
      
      var address = await _context.Addresses.Where(a => a.UserId == user.Id).FirstOrDefaultAsync();
      if ( address is null) return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"Failed to update Address {model.Id}." });
      if ( User.Identity?.Name != user?.UserName ) return Unauthorized("You are not allowed to edit this profile");

      address = model;
      await _context.SaveChangesAsync();

      return Ok(
        new {
          Id = model
        }
      );
    }

    [HttpPost]
    [Route("create/{username}")]
    public async Task<IActionResult> CreateAddress(string username, [FromBody] Address model)
    {
      model.Id = Guid.NewGuid();
      if ( model is null ) return BadRequest("No User was provided.");
      var user = await _userManager.FindByNameAsync(username);
      if ( User.Identity?.Name != user?.UserName ) return Unauthorized("You are not allowed to edit this profile");
      
      model.UserId = user?.Id;
      
      await _context.Addresses.AddAsync(model);
      await _context.SaveChangesAsync();

      return Ok(
         model
      );
    }
  }
}