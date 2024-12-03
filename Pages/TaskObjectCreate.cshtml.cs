using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskEntityObjectCreateModel : PageModel
{   public TaskObject taskObject{get; set;}
    public taskEntityId{get; set;}
    public TaskEntity taskEntity{get; set;}
    public void OnGet(int taskEntityIdInput, int? id)
    {   TaskEntityController taskEntityController = new TaskEntityController();
        taskEntityId = taskEntityIdInput;
        if(id != null){
            // обновить
        }
        
    }

    public void OnPost(string TaskEntityId, string about, string name, string type){
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntity = taskEntityController.getTaskEntityById(TaskEntityId);
        taskEntityController.createTaskObject(taskEntity, type, name, about);
        return RedirectToPage("/TaskEntitySingleRecord?");
    }
    
}
