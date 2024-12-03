namespace TaskEntitys;
public class TaskEntity{
    public int Id{get; set;}
    public string name{get; set;}
    public string? about{get; set;}
    public List<TaskObject> taskObject{get; set;} = new();
   

}

public class TaskObject{
    public int Id{get; set;}
    public string name{get; set;}
    public string type{get; set;}
    public string about{get; set;}
    public int taskId{get; set;}
    public TaskEntity? TaskEntity {get; set;}
    /*public TaskObject(string name, string type, string about){
        this.name = name;
        this.type = type;
        this.about = about;
    }*/
}