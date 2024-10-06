using Microsoft.AspNetCore.Identity;

namespace HockeyClubAPI.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string? Fullname { get; set; } = string.Empty;
    public string? Position { get; set; } = string.Empty;
  }

  public class Player
  {
    public int Id{ get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public int JerseyNumber { get; set; }
    public DateTime DateOfBirth { get; set;}
  }
}

