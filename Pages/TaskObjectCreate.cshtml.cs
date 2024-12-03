using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskEntityObjectCreateModel : PageModel
{   public TaskObject taskObject{get; set;}
    
    public void OnGet(int id)
    {
        if(id != null){
            // обновить
        }        
    }

    public void OnPost(){
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntityController.createTaskEntity(name, about);
        return RedirectToPage("/Task");
    }
    
}
