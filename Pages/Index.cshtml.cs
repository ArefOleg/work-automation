using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;

namespace work_automation.Pages;

public class IndexModel : PageModel
{   public Menu mainMenu;
    

    public IndexModel(ILogger<IndexModel> logger)
    {        
        
    }

    public void OnGet()
    {       
        mainMenu = new Menu();
    }
}
