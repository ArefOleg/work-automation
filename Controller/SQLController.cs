using Microsoft.EntityFrameworkCore;
using SQLSpace;

public class SQLController{
    public void createSQLRec(string name, string about, string sqlBody, int TaskEntityId){
        using (ApplicationContext db = new ApplicationContext()){            
            SQLEntity sQLEntity = new SQLEntity
             { name = name, about = about, sqlBody = sqlBody,
              created = DateTime.Now, TaskEntityId = TaskEntityId};
            db.taskEntities.AddRange(taskEntity);
            db.SaveChanges();
        }
    }
    public void deleteSQLRec(int Id){
        using(ApplicationContext db = new ApplicationContext()){
            db.sqlEntities.Where(p => p.Id == Id).ExecuteDelete();
        }
    }

    public List<SQLEntity> getSQLRecs(){
        var entities = new List<SQLEntity>();
        using(ApplicationContext db = new ApplicationContext()){
            entities = (from entity in db.sqlEntities select entity).ToList();            
        }
        entities = entities.OrderByDescending(e=>e.created).ToList();
        return entities;
    }

    public SQLEntity getSQLEntity(int Id){
        SQLEntity sQLEntity;
        using(ApplicationContext db = new ApplicationContext()){
            sQLEntity = db.sqlEntities.Find(Id);  
        }
        return sQLEntity;
    }

    public string getTaskEntityName(int TaskEntityId){
        string name = "";
        TaskEntity taskEntity;
        using(ApplicationContext db = new ApplicationContext()){
            taskEntity = db.taskEntities.Find(TaskEntityId); 
        }
        name = taskEntity.name;
        return name;

    }
}