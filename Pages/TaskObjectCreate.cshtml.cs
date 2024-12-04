using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskObjectCreateModel : PageModel
{   public TaskObject taskObject{get; set;}
    public int taskEntityId{get; set;}
    public TaskEntity taskEntity{get; set;}
    public string Action{get; set;}
    public void OnGet(int taskEntityIdInput, int? id)
    {   TaskEntityController taskEntityController = new TaskEntityController();
        taskEntityId = taskEntityIdInput;
        if(id != null){
            Action = "update";
            taskObject = taskEntityController.getTaskObjectById((int)id);
        }
        
    }

    public IActionResult OnPost(int TaskEntityIdPost, string about, string name, string type,
    string? Action, int? TaskObjectIdPost){
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntity = taskEntityController.getTaskEntityById(TaskEntityIdPost);
        if(Action.Equals("update")){
            taskEntityController.updateTaskObject(name, about, type, (int)TaskObjectIdPost);
        }else{
            taskEntityController.createTaskObject(taskEntity, type, name, about);
        }        
        return new RedirectToPageResult("/TaskEntitySingleRecord", new {Id = TaskEntityIdPost});
    }
    
}
