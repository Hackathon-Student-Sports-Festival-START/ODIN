using System.ComponentModel.DataAnnotations;

namespace StartOdin.Domain.ViewModels.Auth;

public class RegisterViewModel
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    
    [Required]
    public DateTime? DateOfBirth { get; set; }
    
    [Required]
    public string? Role { get; set; }
    
    [Required]
    public int TeamName { get; set; }
    
    [Required]
    public string? University { get; set; }
    
    [Required]
    public bool IsAgreeWithTerms { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
}