using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;

namespace work_automation.Pages;

public class EaiModel : PageModel
{   public Menu mainMenu;
    private readonly ILogger<IndexModel> _logger;

    public EaiModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        mainMenu = new Menu();
        mainMenu.eaiMenu();
    }

    public void OnGet()
    {

    }
}
