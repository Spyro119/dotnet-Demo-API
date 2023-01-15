using Demo_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Demo_API.Data;


public class DummyData {
  public static async Task Initialize(IServiceProvider serviceProvider, IConfigurationSection Admin) {
    var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
    
    if (userManager == null ) throw new ArgumentNullException("An Error occured : userManager is null.");
    if (roleManager == null ) throw new ArgumentNullException("An Error occured : roleManager is null.");
    
    string password = Admin["Password"] ??  throw new ArgumentNullException("An error occurred : It seems the Admin email is null. Please provide an email in /appsettings.json");
    string email = Admin["Email"] ?? throw new ArgumentNullException("An error occurred : It seems the Admin password is null. Please provide a passord in /appsettings.json");
    string username = email;
    string securityStamp = Guid.NewGuid().ToString();

     if (userManager.Users.Any()) return;
    
    Console.WriteLine("Initializing database... ");
     
    ApplicationUser user = new() {
      Email = email,
      SecurityStamp = securityStamp,
      UserName = username
    };
    
    var userRes = await userManager.CreateAsync(user, password);
      Console.WriteLine(userRes);
      
    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
      await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    if (!await roleManager.RoleExistsAsync(UserRoles.User))
      await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
    if (await roleManager.RoleExistsAsync(UserRoles.Admin)) 
      await userManager.AddToRoleAsync(user, UserRoles.Admin);
    if (await roleManager.RoleExistsAsync(UserRoles.User)) 
      await userManager.AddToRoleAsync(user, UserRoles.User);
    }
}