using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskEntitySingleRecordModel : PageModel
{   public TaskEntity taskEntity{get; set;}
    public void OnGet()
    {     
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntity = taskEntityController.getTaskEntityById(Convert.ToInt32(RouteData.Values["TaskId"]));
        
    }
    public void OnDeleteTaskObject(){
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntityController.deleteTaskObject(Convert.ToInt32(RouteData.Values["TaskObjectId"]));
        taskEntity = taskEntityController.getTaskEntityById(Convert.ToInt32(RouteData.Values["TaskId"]));
    }
    public void OnDeleteSQL(){
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntityController.deleteTaskObject(Convert.ToInt32(RouteData.Values["SQLId"]));
        taskEntity = taskEntityController.getTaskEntityById(Convert.ToInt32(RouteData.Values["TaskId"]));
    }
    
}
