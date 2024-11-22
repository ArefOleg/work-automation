using servcom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace work_automation.Pages;

public class ServManagerModel : PageModel
{   public string Message{get; set;}
    public string svrmgrService{get; set;}
    public string operation{get; set;}
    public string svrmgr{get; set;}
    public string serverName{get; set;}
    public string compName{get; set;}
    public string logLevel{get; set;}
    public void OnGet(string svrmgr)
    {       
       svrmgrService = svrmgr;
       if(svrmgr.Equals("viewToSql")){          
          operation = "Нужно создать файл tables.txt\n"
          + "Указать какие работы с таблицей\n"
          + "Например EX_LOY_0000005:Update или Insert\n"
          + "Комманда: C:\\Siebel\\Tools\\BIN\\siebdev.exe /c C:\\Siebel\\Tools\\BIN\\enu\tools.cfg /l \n"
          + " enu /d ServerDataSrc /u arefev /p arefev /IncrementalTablePublish D:\\AREFEV\\tables.txt";
       }
    }
    public void OnPost(string? svrmgr, string? serverName, string? compName, string? logLevel, string? branch){
        operation = "Заполните данные о логе";
        if(svrmgr.Equals("log")){
            Message = servcom.Server_Commands.setLog(compName, serverName, logLevel);
        }
    }
}
