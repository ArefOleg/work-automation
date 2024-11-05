using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using XML_Header;
using XML_LWS2;

namespace work_automation.Pages;

public class EaiModel : PageModel
{   public Menu mainMenu;
    public string Action{get; set;}
    public List<LineItems> lineItems;
    public LineItems lineItem{get; set;}
    public Order order{get; set;}
    private readonly ILogger<IndexModel> _logger;
    public string Message { get; private set; } = "";
    
    public void OnGet()
    {        
        
    }
    //Создание позиции чека
    public void OnPost(String Action, LineItems? lineItem, Order? order)    
    {
        
        if(Action.Equals("LineItem")){
            lineItem.setAmountAdjusted();
            Task.WaitAll(fillLineItem(lineItem));
        } else if(Action.Equals("Order")){
            var task = Task.Run(async () => await getAllLineItemsForOrderAsync());
            task.Wait();
            List<LineItems> lineItemsInner = task.Result;
            order.setFields(lineItemsInner);
            Task.WaitAll(clearJSONFile());
            Message = generateLWS2XML.generate(order);            
        } else {}
         
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
}