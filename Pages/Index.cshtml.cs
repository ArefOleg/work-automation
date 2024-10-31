using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;

namespace work_automation.Pages;

public class IndexModel : PageModel
{   public Menu mainMenu;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        mainMenu = new Menu();
    }

    public void OnGet(string menu)
    {
        if(menu.Equals("EAI")) mainMenu.eaiMenu();
    }
}
