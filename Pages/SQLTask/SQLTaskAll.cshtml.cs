using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using TaskEntitys;
using SQLSpace;

namespace work_automation.Pages;

public class SQLTaskAllModel : PageModel
{   
    public List<SQLEntityExtended> sQLEntityExtendeds{get; set;}
    public void OnGet()
    {
        sQLEntityExtendeds = new List<SQLEntityExtended>();
        List<SQLEntity> sqlEntities = new List<SQLEntity>();
        SQLController sqlController = new SQLController();
        sqlEntities = sqlController.getAllSQL();                    
        foreach(var sQLEntity in sqlEntities){
            SQLEntityExtended sQLEntityExtended =
             new SQLEntityExtended(sqlController, sQLEntity);
            sQLEntityExtendeds.Add(sQLEntityExtended);
        }
    }
    
}
