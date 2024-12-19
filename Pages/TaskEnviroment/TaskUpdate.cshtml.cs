using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;
namespace work_automation.Pages;

public class TaskUpdateModel : PageModel
{   
    public TaskEntity taskEntity{get; set;}
    public int TaskId{get; set;}
    public void OnGet()
    {   
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntity = taskEntityController.getTaskEntityById(Convert.ToInt32(RouteData.Values["Id"]));
    }
    public IActionResult OnPost(){
        
        return RedirectToPage("Task");
    }
}
