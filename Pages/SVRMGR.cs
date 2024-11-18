using svrmgr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace work_automation.Pages;

public class SVRMGRModel : PageModel
{   public string Message{get; set;}
    public string svrmgrService{get; set;}
    
    public void OnGet(string svrmgr)
    {       
       svrmgrService = svrmgr;
       if(svrmgr.Equals("log")){          
          Message = Server_Commands.setLog();
       }
    }
    public void OnPost(){
        
    }
}
