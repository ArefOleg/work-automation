using System.Text;
using System.Text.Json;
using System.Web;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using XML_Header;
using XML_LWS2;
using XML_LWS4;
using XML_LWS8;

namespace work_automation.Pages;

public class EaiModel : PageModel
{   public Menu mainMenu;
    public string Action{get; set;}
    public string integrationService{get; set;} 
    public List<LineItems> lineItems;
    public LineItems lineItem{get; set;}
    public Order order{get; set;}
    public JETLWS4OrderCancel_Input jETLWS4OrderCancel_Input{get; set;}
    public JETLWS8GetTransactions_1_Input jETLWS8GetTransactions_1_Input{get; set;}
    private readonly ILogger<IndexModel> _logger;
    public string Message { get; set; } = "";
    public string URL{get; set;} = "";
    
    public void OnGet(string service)
    {        
        integrationService = service;
    }
    //Создание позиции чека
    public void OnPost(string service, String Action, LineItems? lineItem, Order? order,
    JETLWS4OrderCancel_Input? jETLWS4OrderCancel_Input,
    JETLWS8GetTransactions_1_Input? jETLWS8GetTransactions_1_Input)    
    {   integrationService = service;
        
        if(Action.Equals("LineItem")){
            lineItem.setAmountAdjusted();
            Task.WaitAll(fillLineItem(lineItem));
        } else if(Action.Equals("Order")){
            var task = Task.Run(async () => await getAllLineItemsForOrderAsync());
            task.Wait();
            List<LineItems> lineItemsInner = task.Result;
            order.setFields(lineItemsInner);
            Task.WaitAll(clearJSONFile());
            Task.WaitAll(clearXML());
            Message = generateLWS2XML.generate(order); 
            service = "LWS2";
            URL = "DEV https://msk03-sbldev2.licard.com/siebel/app/eai_teboil/rus?SWEExtSource=WebService&SWEExtCmd=Execute&WSSOAP=1<p>"
            + "TEST https://msk03-sbl2-tt1.licard.com/siebel/app/eai/enu?SWEExtSource=WebService&SWEExtCmd=Execute&WSSOAP=1"
            + "PRE https://msk03-sw3-pre.licard.com:9001/siebel/app/eai/enu?SWEExtSource=WebService&SWEExtCmd=Execute&WSSOAP=1";
               
        } else if(Action.Equals("LWS4")){
            Task.WaitAll(clearXML());
            Message = generateLWS4XML.generate(jETLWS4OrderCancel_Input);
            service = "LWS4";
        } else if(Action.Equals("LWS8")){
            Task.WaitAll(clearXML());
            Message = generateLWS8XML.generate(jETLWS8GetTransactions_1_Input);
            service = "LWS8";
        }
         
    }

    public async Task fillLineItem(LineItems lineItem){
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
             FileMode.OpenOrCreate)){
            lineItems = new List<LineItems>();
            if(new FileInfo("wwwroot/sources/menu/line item.json").Length == 0){                
                lineItems.Add(lineItem);                
            } else{                         
                lineItems = await JsonSerializer.DeserializeAsync<List<LineItems>>(fs);     
                fs.SetLength(0);                
                lineItems.Add(lineItem);
            }
            await JsonSerializer.SerializeAsync<List<LineItems>>(fs, lineItems);
            fs.Close();
        } 
    }

    public async Task<List<LineItems>> getAllLineItemsForOrderAsync(){
        List<LineItems> lli;
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
            FileMode.OpenOrCreate)){
            lli = await JsonSerializer.DeserializeAsync<List<LineItems>>(fs);                        
        }
        return lli;
    }

    public List<LineItems> getAllLineItemsForOrder(){
        var task = Task.Run(async () => await getAllLineItemsForOrderAsync());
        task.Wait();
        return task.Result;
    }
    public async Task clearJSONFile(){
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
            FileMode.OpenOrCreate)){
            fs.SetLength(0);
            fs.Close();
        }        
    }

    public async Task clearXML(){
        using (FileStream fs = new FileStream("wwwroot/sources/menu/xml.xml",
            FileMode.OpenOrCreate)){
            fs.SetLength(0);
            fs.Close();
        }        
    }

}