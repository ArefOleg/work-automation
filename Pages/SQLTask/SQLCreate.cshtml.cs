using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;
using SQLSpace;
namespace work_automation.Pages;

public class SQLCreateModel : PageModel
{   
    [BindProperty]
    public SQLSpace.SQLEntity sQLEntity{get; set;} = new();    
    public string Action{get; set;}
    public void OnGet()
    {   
        
    }    
    /*public void OnUpdate(){
        /*SQLController sQLController = new SQLController();
        Action = "update";
        sQLEntity = sQLController.getSQLEntity(Convert.ToInt32(RouteData.Values["id"]));
    }    */

    public IActionResult OnPost(){
        SQLController sQLController = new SQLController();
        sQLController.createSQL(sQLEntity);
        return new RedirectToPageResult("/TaskEnviroment/TaskEntitySingleRecord", new {Id = TaskEntityId});
    }   
}
