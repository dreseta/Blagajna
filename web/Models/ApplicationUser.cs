using Microsoft.AspNetCore.Identity;
namespace web.Models;
public class ApplicationUser : IdentityUser
{
    // First Name
    public string? FirstName { get; set; }

    // Last Name
    public string? LastName { get; set; }
}
