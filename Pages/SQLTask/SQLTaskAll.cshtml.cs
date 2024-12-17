using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;
using SQLSpace;

namespace work_automation.Pages;

public class SQLTaskAllModel : PageModel
{   public List<SQLEntity> sqlEntities{get; set;}
    public void OnGet()
    {
        SQLController sqlController = new SQLController();
        sqlEntities = sqlController.getAllSQL();                    
        
    }
    
}
