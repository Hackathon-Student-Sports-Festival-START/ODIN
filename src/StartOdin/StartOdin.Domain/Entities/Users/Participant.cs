namespace StartOdin.Domain.Entities.Users;

public class Participant
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Имя участника
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Фамилия участника
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя в Telegram
    /// </summary>
    public string? TelegramId { get; set; }
    
    /// <summary>
    /// Номер телефона участника
    /// </summary>
    public string? Phone { get; set; }
    
    /// <summary>
    /// Электронная почта участника
    ///
    /// Авторизация участника происходит по электронной почте
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Идентификатор команды пользователя
    /// </summary>
    public int TeamId { get; set; }
    
    /// <summary>
    /// Роль игрока в команде
    /// </summary>
    public string? Role { get; set; }
    
}