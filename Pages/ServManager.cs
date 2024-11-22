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
    public string branch{get; set;}
    public string warning{get; set;}
    public string param{get; set;}
    public string value{get; set;}
    public void OnGet(string svrmgr)
    {       
       svrmgrService = svrmgr;
       if(svrmgr.Equals("viewToSql")){          
          Message = "Нужно создать файл tables.txt\n"
          + "Указать какие работы с таблицей\n"
          + "Например EX_LOY_0000005:Update или Insert\n"
          + "Комманда: C:\\Siebel\\Tools\\BIN\\siebdev.exe /c C:\\Siebel\\Tools\\BIN\\enu\tools.cfg /l \n"
          + " enu /d ServerDataSrc /u arefev /p arefev /IncrementalTablePublish D:\\AREFEV\\tables.txt";
       }
    }
    public void OnPost(string? svrmgr, string? serverName, string? compName, string? logLvl,
     string? branch, string? param, string? value){
        svrmgrService = svrmgr;
        operation = "Заполните данные о логе";
        if(svrmgr.Equals("log")){
            Message = servcom.Server_Commands.setLog(compName, serverName, logLvl);
        } else if(svrmgr.Equals("getSessions")){
            Message = servcom.Server_Commands.getSessionForComp(compName, serverName);
        } else if(svrmgr.Equals("getParameters")){
            Message = servcom.Server_Commands.getSessionForComp(compName, serverName);
        } else if(svrmgr.Equals("setBranch")){
            Message = servcom.Server_Commands.changeBrancheForComp(compName, serverName, branch);
        } else if(svrmgr.Equals("getBranch")){
            Message = servcom.Server_Commands.getBrancheFromComp(compName, serverName);
        }else if(svrmgr.Equals("changeParam")){
            warning = "Нужно менять на определении компоненты или самой компоненты\n" +
            "Если поменять на определении компоненты, то поменяется сразу на всех созданных от нее компоненты";
            Message = servcom.Server_Commands.setParamForComp(compName, serverName, param, value);
        }
        
    }
}
