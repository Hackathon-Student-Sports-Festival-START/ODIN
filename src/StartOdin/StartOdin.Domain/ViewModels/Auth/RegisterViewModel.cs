namespace StartOdin.Domain.ViewModels.Auth;

public class RegisterViewModel
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Role { get; set; }
    public int TeamId { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}