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
    public string getTaskEntityName(SQLController sQLController){
        string TaskEntityName = sQLController.getTaskEntityName(this.TaskEntityId);
        return TaskEntityName;
    }
}

public class SQLEntityExtended : SQLEntity{
    public string taskEntityName{get; set;}
    public SQLEntityExtended(SQLController sQLController, SQLEntity sQLEntity){
        this.about = sQLEntity.about;
        this.name = sQLEntity.name;
        this.created = sQLEntity.created;
        this.Id = sQLEntity.Id;
        this.sqlBody = sQLEntity.sqlBody;
        this.taskEntityName = sQLController.getTaskEntityName(sQLEntity.TaskEntityId);
    }
    public void setTaskEntityName(SQLController sQLController){
        this.taskEntityName = sQLController.getTaskEntityName(this.TaskEntityId);
    }
}