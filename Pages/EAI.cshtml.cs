using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
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
        Console.WriteLine(Action);
        Message = "OnPost";
        lineItem.setAmountAdjusted();
        Task.WaitAll(fillLineItem(lineItem));

        Task.WaitAll(clearJSONFile());
    }
    //Создание чека
    /*public void OnPost(string lineItemAmount, string CardNumber, string OrderType,
    string Attrib1, string DiscountCUR, string TerminalId, string AcquiringId){
        Order order = new Order(lineItemAmount, CardNumber, OrderType, Attrib1,
        DiscountCUR, TerminalId, AcquiringId);
    }*/


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
            //Написать сеттеры
            fs.Close();
        } 
    }

    public async Task<List<LineItems>> getAllLineItemsForOrderAsync(){
        List<LineItems> lli;
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
            FileMode.OpenOrCreate)){
            lli = await JsonSerializer.DeserializeAsync<List<LineItems>>(fs);
            Task.WaitAll(clearJSONFile());            
        }
        return lli;
    }
    public async Task clearJSONFile(){
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
            FileMode.OpenOrCreate)){
            fs.SetLength(0);
            fs.Close();
        }
        
    }
}