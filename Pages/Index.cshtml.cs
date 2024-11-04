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
       }
    }
}
