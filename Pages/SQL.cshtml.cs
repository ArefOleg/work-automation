using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using SQL_Utils;
namespace work_automation.Pages;

public class SQLModel : PageModel
{   public string Message{get; set;}
    public string sqlService{get; set;}
    
    public void OnGet(string? sql = "none")
    {       
       sqlService = sql;
       if(sql.Equals("declare")){
          var task = Task.Run(async () => await SQL_Utilities.getDeclare());
          task.Wait();
          Message = task.Result;
       }else if(sql.Equals("createTable")){
          var task = Task.Run(async () => await SQL_Utilities.getCreateTable());
          task.Wait();
          Message = task.Result;
       }else if(sql.Equals("copyTable")){
          var task = Task.Run(async () => await SQL_Utilities.getCopyTable());
          task.Wait();
          Message = task.Result;
       }else if(sql.Equals("rep_generation")){
         var task = Task.Run(async () => await SQL_Utilities.getReportGeneration());
         task.Wait();
         Message = task.Result;
       }
       else if(sql.Equals("getDate")){         
         Message = SQL_Utilities.toDate();
       }
    }

    public void OnPost(string? sql, string? date){
      sqlService = sql;
      Message = SQL_Utilities.getSMACheck(date);
    }
}
