using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;
using SQLSpace;
namespace work_automation.Pages;

public class SQLCreateModel : PageModel
{   public SQLEntity sQLEntity{get; set;}
    public int taskEntityId{get; set;}
    public TaskEntity taskEntity{get; set;}
    public string Action{get; set;}
    public void OnGet(int taskEntityIdInput, int? id)
    {   SQLController sQLController = new SQLController();
        taskEntityId = taskEntityIdInput;
        
        if(id != null){
            Action = "update";
            sQLEntity = sQLController.getSQLEntity((int)id);
        } else Action = "insert";
        
    }

    public IActionResult OnPost(string? name, string? about, 
    string? sqlBody, int? TaskEntityId, int? Id,
    string ActionInput){
        SQLController sQLController = new SQLController();
        if(ActionInput.Equals("update")){
            //taskEntityController.updateTaskObject(name, about, type, (int)TaskObjectIdPost);
        }else{
            sQLController.createSQLRec(name, about, sqlBody, (int)TaskEntityId);
        }        
        return new RedirectToPageResult("/TaskEntitySingleRecord", new {Id = TaskEntityId});
    }
    
}
