namespace StartOdin.Domain.Entities.Users;

public class Admin
{
    /// <summary>
    /// Идентификатор администратора в системе
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Имя пользователя в системе
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Пароль пользователя в системе в формате SHA256
    /// </summary>
    public string? Password { get; set; }

    public string? Role { get; set; }
}