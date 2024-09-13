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
    public string? TeamName { get; set; }
    
    /// <summary>
    /// Роль игрока в команде
    /// </summary>
    public string? Role { get; set; }
    
    /// <summary>
    /// Пароль участника
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Образовательное учебное заведение участника
    /// </summary>
    public string? University { get; set; }
    
    /// <summary>
    /// Дата рождения участника
    /// </summary>
    public DateTime DateOfBirth { get; set; }
}