using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskEntitySingleRecordModel : PageModel
{   public TaskEntity taskEntity{get; set;}
    
    public void OnGet(int id)
    {
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntity = taskEntityController.getTaskEntityById(id);
    }
    
}
