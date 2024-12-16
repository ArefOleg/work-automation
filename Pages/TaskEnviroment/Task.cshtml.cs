using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskModel : PageModel
{   public List<TaskEntity> taskEntities{get; set;}
    
    public void OnGet(int? deleteId)
    {
        TaskEntityController taskEntityController = new TaskEntityController();
        if(deleteId != null){
            taskEntityController.deleteTaskEntity((int)deleteId);
        }
        
        taskEntities = taskEntityController.getTaskEntities();        
    }
    
}
