using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskCreateModel : PageModel
{   
    
    
    public void OnGet()
    {
        
    }
    public IActionResult OnPost(string name, string about){
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntityController.createTaskEntity(name, about);
        return RedirectToPage("/Task");
    }
}
