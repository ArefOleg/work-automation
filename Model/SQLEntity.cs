using TaskEntitys;
namespace SQLSpace;

public class SQLEntity{
    public int Id{get; set;}
    public string name{get; set;}
    public string about{get; set;}
    public string sqlBody{get; set;}
    public DateTime created{get; set;}
    public int TaskEntityId{get; set;}
    public TaskEntity? TaskEntity {get; set;}

}