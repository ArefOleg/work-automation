using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskEntitySingleRecordModel : PageModel
{   public TaskEntity taskEntity{get; set;}
    
    public void OnGet(int id, int? deleteTaskObjectById)
    {
        TaskEntityController taskEntityController = new TaskEntityController();
        if(deleteTaskObjectById != null){
            taskEntityController.deleteTaskObject((int)deleteTaskObjectById);
        }
        taskEntity = taskEntityController.getTaskEntityById(id);
    }
    
}
