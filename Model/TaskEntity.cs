using SQLSpace;
namespace TaskEntitys;

public class TaskEntity{
    public int Id{get; set;}
    public DateTime created{get; set;}
    public string name{get; set;}
    public string? about{get; set;}
    public List<TaskObject> TaskObject{get; set;} = new();
    public List<SQLEntity> SQL{get; set;} = new();

}

public class TaskObject{
    public int Id{get; set;}
    public string name{get; set;}
    public string type{get; set;}
    public string about{get; set;}
    public int TaskEntityId{get; set;}
    public TaskEntity? TaskEntity {get; set;}
    
}