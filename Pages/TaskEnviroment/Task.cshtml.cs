using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskModel : PageModel
{   public List<TaskEntity> taskEntities{get; set;}
    
    public void OnGet()
    {
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntities = taskEntityController.getTaskEntities();        
    }

    public void OnGetDelete(){
        TaskEntityController taskEntityController = new TaskEntityController();
        Console.WriteLine("DELETE");
        taskEntityController.deleteTaskEntity(Convert.ToInt32(RouteData.Values["TaskId"]));
        taskEntities = taskEntityController.getTaskEntities();
    }
    
}
