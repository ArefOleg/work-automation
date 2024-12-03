using Microsoft.EntityFrameworkCore;
using TaskEntitys;
public class TaskEntityController{
    public void createTaskEntity(string name, string about){        
        using (ApplicationContext db = new ApplicationContext()){            
            TaskEntity taskEntity = new TaskEntity { name = name, about = about, created = DateTime.Now};
            db.taskEntities.AddRange(taskEntity);
            db.SaveChanges();
        }
    }
    public List<TaskEntity> getTaskEntities(){        
        var entities = new List<TaskEntity>();
        using(ApplicationContext db = new ApplicationContext()){
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            entities = (from entity in db.taskEntities select entity).ToList();            
        }
        entities = entities.OrderByDescending(e=>e.created).ToList();
        return entities;
    }
    public void deleteTaskEntity(int Id){
        using(ApplicationContext db = new ApplicationContext()){
            db.taskEntities.Where(p => p.Id == Id).ExecuteDelete();
        }
    }

    public TaskEntity getTaskEntityById(int Id){
        TaskEntity taskEntity;
        using(ApplicationContext db = new ApplicationContext()){
            taskEntity = db.taskEntities.Find(Id);
        }
        return taskEntity;
    }
    public void createTaskObject(TaskEntity taskEntity, string type, string name, string about){
        using (ApplicationContext db = new ApplicationContext()){            
            TaskObject taskObject = new TaskObject { name = name, about = about,
             type = type, TaskEntity = taskEntity};       
            db.taskObjects.AddRange(taskObject);
            db.SaveChanges();
        }
    }

    
}