using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;
using SQLSpace;

namespace work_automation.Pages;

public class SQLTaskAllModel : PageModel
{   public List<SQLEntity> sqlEntities{get; set;}
    public void OnGet(int? deleteId)
    {
        SQLController sqlController = new SQLController();
        if(deleteId != null){
            sqlController.deleteSQLRec((int)deleteId);
        }
        
        sqlEntities = sqlController.getAllSQL();
             
    }
    
}
