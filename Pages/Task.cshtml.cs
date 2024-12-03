using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;

namespace work_automation.Pages;

public class TaskModel : PageModel
{   public int taskCount{get; set;}
    public List<TaskEntity> taskEntities{get; set;}
    
    public void OnGet()
    {
        taskCount = Directory.GetFiles("wwwroot/task", "*", SearchOption.TopDirectoryOnly).Length;
        TaskEntityController taskEntityController = new TaskEntityController();
        taskEntities = taskEntityController.getTaskEntities();
        foreach (TaskEntity taskon in taskEntities){
            Console.WriteLine(taskon.name);
        }
    }
}
