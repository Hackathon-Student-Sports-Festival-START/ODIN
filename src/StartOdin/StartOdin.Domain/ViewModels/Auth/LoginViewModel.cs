using System.ComponentModel.DataAnnotations;

namespace StartOdin.Domain.ViewModels.Auth;

public class LoginViewModel
{
    [Required]
    [DataType(DataType.Text)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    public bool RememberMe { get; set; }
}