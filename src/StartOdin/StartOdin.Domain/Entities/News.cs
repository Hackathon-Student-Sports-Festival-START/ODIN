namespace StartOdin.Domain.Entities;

public class News
{
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Content { get; set; }
    
    public DateTime DateTime { get; set; }
}