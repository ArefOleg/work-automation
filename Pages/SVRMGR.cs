using svrmgr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace work_automation.Pages;

public class SVRMGRModel : PageModel
{   public string Message{get; set;}
    public string svrmgrService{get; set;}
    public string operation{get; set;}
    public void OnGet(string svrmgr)
    {       
       svrmgrService = svrmgr;
       if(svrmgr.Equals("log")){          
          operation = "Заполните данные о логе";
       }
    }
    public void OnPost(string serverName, string compName, int logLevel){
        
    }
}
