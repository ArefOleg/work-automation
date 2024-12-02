public class TaskEntity{
    public string name{get; set;}
    public string file{get; set;}
    public string about{get; set;}
    public List<TaskObject> taskObject;
    public TaskEntity(string name, string file){
        this.name = name;
        this.file = file;
        taskObject = new List<TaskObject>();
    }

}

public class TaskObject{
    public string name{get; set;}
    public string type{get; set;}
    public string about{get; set;}
    public TaskObject(string name, string type, string about){
        this.name = name;
        this.type = type;
        this.about = about;
    }
}