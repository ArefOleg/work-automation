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
           // db.Database.EnsureDeleted();
           // db.Database.EnsureCreated();
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
            taskEntity.TaskObject =  getTaskObjetctsByEntityId(Id);    
        }
        return taskEntity;
    }

    public void updateTaskEntity(string name, string about, int id){
        using(ApplicationContext db = new ApplicationContext()){
            db.taskEntities.Where(te=>te.Id == id).
            ExecuteUpdate(te=>te.SetProperty(t=>t.name, t => name)
            .SetProperty(t=>t.about, t => about));
        }
    }
    public void createTaskObject(TaskEntity taskEntity, string type, string name, string about){
        using (ApplicationContext db = new ApplicationContext()){            
            TaskObject taskObject = new TaskObject { name = name, about = about,
             type = type, TaskEntityId = taskEntity.Id};       
            db.taskObjects.AddRange(taskObject);
            db.SaveChanges();
        }
    }

    public List<TaskObject> getTaskObjetctsByEntityId(int taskEntityId){
        List<TaskObject> taskObjects;
        using(ApplicationContext db = new ApplicationContext()){
            taskObjects = (from entityObject in db.taskObjects
             where entityObject.TaskEntityId == taskEntityId
             select entityObject).ToList();  
        }
        return taskObjects;
    }

    public void deleteTaskObject(int Id){
        using(ApplicationContext db = new ApplicationContext()){
            db.taskObjects.Where(p => p.Id == Id).ExecuteDelete();
        }
    }
}
