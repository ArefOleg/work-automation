using TaskEntitys;
public class TaskEntityController{
    public void createTaskEntity(string name, string about){        
        using (ApplicationContext db = new ApplicationContext()){
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            TaskEntity taskEntity = new TaskEntity { name = name, about = about};
            db.taskEntities.AddRange(taskEntity);
            db.SaveChanges();
        }
    }
    public List<TaskEntity> getTaskEntities(){
        var entities = new List<TaskEntity>();
        using(ApplicationContext db = new ApplicationContext()){
            entities = (from entity in db.taskEntities select entity).ToList();
        }
        return entities;
    }
    public void createTaskObject(){

    }

    
}