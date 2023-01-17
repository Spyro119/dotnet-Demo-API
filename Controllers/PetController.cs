using Demo_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo_API.Data;

namespace Demo_API.Controllers {

  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  public class PetController : ControllerBase {
     private readonly IAuthorizationService _authorizationService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

     public PetController( IAuthorizationService authorizationService,  ApplicationDbContext context, UserManager<ApplicationUser> userManager ) {
       
        _authorizationService = authorizationService;
        _userManager = userManager;
        _context = context;
    }
    // TODO : Implement routes
  }
}