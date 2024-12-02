public class TaskEntity{
    public string name{get; set;}
    public string file{get; set;}
    public string about{get; set;}
    public List<Task_Object> taskObject;
    public TaskEntity(string name, string file){
        this.name = name;
        this.file = file;
        taskObject = new List<Task_Object>();
    }

}

public class Task_Object{
    public string name{get; set;}
    public string type{get; set;}
    public string about{get; set;}
    public Task_Object(string name, string type, string about){
        this.name = name;
        this.type = type;
        this.about = about;
    }
}