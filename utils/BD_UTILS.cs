using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskEntitys;
using SQLSpace;
public class ApplicationContext : DbContext {
    public DbSet<TaskEntity> taskEntities {get; set;} = null!;
    public DbSet<TaskObject> taskObjects {get; set;} = null!;
    public DbSet<SQLEntity> sqlEntities {get; set;} = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=wwwroot/sources/db/prod.db");
    }    
}


