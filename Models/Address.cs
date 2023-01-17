using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_API.Models {
   public class Address
  {
    [Key]
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public string Country { get; set; } = "Canada";
    public string Province { get; set; } = "Qc";
    public string? City { get; set; }
    public string? Street { get; set; }
    public int? CivicNumber { get; set; }
    public string? PostalCode { get; set; }

    [ForeignKey("UserId")]
    public virtual ApplicationUser? ApplicationUser { get; set; }
  }
}