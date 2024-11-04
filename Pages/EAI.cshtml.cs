using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Work_Menu;
using XML_LWS2;

namespace work_automation.Pages;

public class EaiModel : PageModel
{   public Menu mainMenu;
    public List<LineItems> lineItems;
    public LineItems lineItem{get; set;}
    private readonly ILogger<IndexModel> _logger;
    public string Message { get; private set; } = "";
    
    public void OnGet()
    {        
        
    }
    //Создание позиции чека
    public void OnPost(LineItems lineItem)    
    {
        Message = "OnPost";
        Console.WriteLine(lineItem.PumpNum);
        Task.WaitAll(fillLineItem(lineItem));
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
    public async Task clearJSONFile(){
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
            FileMode.OpenOrCreate)){
            fs.SetLength(0);
            fs.Close();
        }
        
    }
}