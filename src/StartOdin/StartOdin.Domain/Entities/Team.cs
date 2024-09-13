using System.ComponentModel.DataAnnotations;

namespace StartOdin.Domain.Entities;

public class Team
{
    /// <summary>
    /// Идентификатор команды
    /// </summary>
    [Required]
    public int Id { get; set; }
    
    /// <summary>
    /// Имя команды
    /// </summary>
    public string? Name {get; set;}
    
    /// <summary>
    /// Описание команды
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Путь к файлу с логотипом
    /// </summary>
    public string? LogoPath {get; set;}
    
    /// <summary>
    /// Дисциплина по которой выступает команда
    /// </summary>
    public string? Discipline {get; set;}
    
    /// <summary>
    /// Количество участников команды
    /// </summary>
    public int? MembersCount {get; set;}
    
    /// <summary>
    /// Золотые медали
    /// </summary>
    public int? GoldMedals { get; set; }
    
    /// <summary>
    /// Серебрянные медали
    /// </summary>
    public int? SilverMedals { get; set; }
    
    /// <summary>
    /// Бронзовые медали
    /// </summary>
    public int? BronzeMedals { get; set; }
}