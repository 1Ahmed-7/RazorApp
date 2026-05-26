using System.ComponentModel.DataAnnotations;

namespace ComprehensiveBlazorApp.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(0.0, 4.0)]
    public double Gpa { get; set; }

    public DateTime EnrollmentDate { get; set; } = DateTime.Today;
}
