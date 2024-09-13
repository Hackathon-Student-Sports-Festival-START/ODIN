using Microsoft.EntityFrameworkCore;
using StartOdin.Domain.Entities;
using StartOdin.Domain.Entities.Users;

namespace StartOdin.Core.Database;

public class DatabaseController : DbContext
{
    #region Singleton

    private static DatabaseController _instance;
    public static DatabaseController GetInstance()
    {
        return _instance ?? (_instance = new DatabaseController());
    }

    #endregion

    #region Public Tables

    /// <summary>
    /// Все администраторы системы
    /// </summary>
    public DbSet<Admin> Admins { get; set; }
    
    
    /// <summary>
    /// Все участники фестиваля
    /// </summary>
    public DbSet<Participant?> Participants { get; set; }
    
    /// <summary>
    /// Все зарегистрированные команды
    /// </summary>
    public DbSet<Team> Teams { get; set; }
    
    #endregion
    
    #region Constructor
    
    private DatabaseController()
    {
#if DEBUG
        // Database.EnsureDeleted();
        Database.EnsureCreated();
#endif
#if !DEBUG
        Database.EnsureCreated();
#endif
    }
    
    #endregion

    #region Protected Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            $"server=87.242.107.68;" +
            $"user=api;" +
            $"password=EjbFmbFebcKH;" +
            $"database=start;",
            new MySqlServerVersion(new Version(8, 0, 33)));
        
    }

    
    
    #endregion
}