using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using XML_LWS2;

namespace work_automation.Pages;

public class EaiModel : PageModel
{   public Menu mainMenu;
    public List<LineItems> lineItems;
    private readonly ILogger<IndexModel> _logger;
    public string Message { get; private set; } = "";
    public EaiModel(ILogger<IndexModel> logger)
    {
        Message = "Введите свое имя";
        lineItems = new List<LineItems>();
    }

    public void OnGet()
    {
        
        Message = "Введите свое имя";
    }
    public void OnPost(string? PumpNum, string LineNumber, string Product, string NetPrice,
    string QuantityRequested)
    {
        if(PumpNum is not null){
            LineItems lineItemsOne = new LineItems(PumpNum, LineNumber, Product,
            NetPrice, QuantityRequested);
            lineItems.Add(lineItemsOne);
        }
        

    }
}
