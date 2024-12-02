using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext {
    public DbSet<TaskEntity> taskEntities {get; set;} = null;
    public DbSet<TaskObject> taskObjects {get; set;} = null;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }    
}


