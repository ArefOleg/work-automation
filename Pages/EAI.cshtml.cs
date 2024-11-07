using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using XML_Header;
using XML_LWS2;
using XML_LWS4;

namespace work_automation.Pages;

public class EaiModel : PageModel
{   public Menu mainMenu;
    public string Action{get; set;}
    public string integrationService{get; set;} 
    public List<LineItems> lineItems;
    public LineItems lineItem{get; set;}
    public Order order{get; set;}
    public JETLWS4OrderCancel_Input jETLWS4OrderCancel_Input{get; set;}
    private readonly ILogger<IndexModel> _logger;
    public string Message { get; private set; } = "";
    public string URL{get; set;} = "";
    
    public void OnGet(string service)
    {        
        integrationService = service;
    }
    //Создание позиции чека
    public void OnPost(string service, String Action, LineItems? lineItem, Order? order,
    JETLWS4OrderCancel_Input? jETLWS4OrderCancel_Input)    
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
        } else if(Action.Equals("LWS4")){
            Task.WaitAll(clearXML());
            Message = generateLWS4XML.generate(jETLWS4OrderCancel_Input);
            service = "LWS4";
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