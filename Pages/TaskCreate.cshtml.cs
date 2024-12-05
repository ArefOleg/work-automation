using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskCreateModel : PageModel
{   public TaskEntity taskEntity{get; set;}
    public string Action {get; set;}
    
    public void OnGet(int? TaskEntityId)
    {   
        TaskEntityController taskEntityController = new TaskEntityController();
        
        if(TaskEntityId != null){
            Action = "update";
            taskEntity = taskEntityController.getTaskEntityById((int)TaskEntityId);
        }else{
            Action = "insert";
        }
    }
    public IActionResult OnPost(string name, string about, string ActionInsert, int id){
        TaskEntityController taskEntityController = new TaskEntityController();
        if(ActionInsert.Equals("update")){
            taskEntityController.updateTaskEntity(name, about, id);
        } else{
            taskEntityController.createTaskEntity(name, about);
        }
        
        return RedirectToPage("/Task");
    }
}
