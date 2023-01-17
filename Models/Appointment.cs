using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_API.Models 
{
  public class Appointment 
  {
    [Key]
    public Guid Id { get; set; }
    public string? ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [ForeignKey("ClientId")]
    public virtual ApplicationUser Client { get; set;} = null!;
    
        
  }

}