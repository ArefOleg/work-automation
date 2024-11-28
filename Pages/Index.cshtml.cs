using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;

namespace work_automation.Pages;

public class IndexModel : PageModel
{   public Menu mainMenu;
    
    
    public void OnGet(string? menu = "main")
    {       
       mainMenu = new Menu();
       if(menu.Equals("eai")){
            mainMenu.eaiMenu();
       } else if(menu.Equals("info")){
            mainMenu.infoMenu();
       }else if(menu.Equals("sql")){
            mainMenu.sqlMenu();
       }else if(menu.Equals("svrmgr")){
            mainMenu.svrmgrMenu();
       }else if(menu.Equals("simple_sql")){
            mainMenu.simpleSqlMenu();
       }
    }
}
