using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_API.Models 
{
  public class Pet 
  {
    [Key]
    public Guid Id { get; set; }
    public string? OwnerId { get; set; }
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Breed { get; set; }
    public string? AdditionnalNotes { get; set; }

    [ForeignKey("OwnerId")]
    public virtual ApplicationUser Owner { get; set; } = null!;


    public virtual int Age {
      get {
        var now = DateTime.Now;
        int age = now.Year - BirthDate.Year;

        if (now.Month < BirthDate.Month || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
          age--;

        return age;
      }
    }
  }
}