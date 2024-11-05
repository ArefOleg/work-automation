using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using XML_LWS2;
namespace work_automation.Pages;

public class TestModel : PageModel
{   public LineItems lineItems{get; set;}
    
    
    public void OnGet()
    {       
       
    }
    public void OnPost(LineItems lineItems){
        Console.WriteLine($"Test {lineItems.PumpNum}");
    }
}
