using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;

namespace work_automation.Pages;

public class TaskModel : PageModel
{   public int taskCount{get; set;}
    
    
    public void OnGet()
    {
        taskCount = Directory.GetFiles("wwwroot/task", "*", SearchOption.TopDirectoryOnly).Length;
    }
}
