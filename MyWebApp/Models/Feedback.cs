namespace MyWebApp.Models;

using System.ComponentModel.DataAnnotations;


    public class Feedback
{
    [Key]
    [StringLength(255)]
    [Required]
    public string EmailId { get; set; }

    public int OverallRating { get; set; }

    public int CleanlinessRating { get; set; }

    public int FacilitiesRating { get; set; }

    public int AccessibilityRating { get; set; }

    [MaxLength(500)]
    [Required]
    public string Suggestions { get; set; }
}


