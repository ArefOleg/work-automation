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
    private readonly ILogger<IndexModel> _logger;
    public string Message { get; private set; } = "";
    public EaiModel(ILogger<IndexModel> logger)
    {
        Message = "Введите свое имя";
        
    }

    public void OnGet(string? PumpNum, string? LineNumber, string? Product, string? NetPrice,
    string? QuantityRequested)
    {        
        Message = "OnGet";
    }
    //Создание позиции чека
    public void OnPost(string? PumpNum, string? LineNumber, string? Product, string? NetPrice,
    string? QuantityRequested)    
    {
        Message = "OnPost";
        Task.WaitAll(fillLineItem(PumpNum, LineNumber, Product, NetPrice, QuantityRequested));
    }
    //Создание чека
    public void OnPost(string lineItemAmount, string CardNumber, string OrderType,
    string Attrib1, string DiscountCUR, string TerminalId, string AcquiringId){

    }


    public async Task fillLineItem(string? PumpNum, string? LineNumber, string? Product, string? NetPrice,
    string? QuantityRequested){
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
             FileMode.OpenOrCreate)){
            LineItems lineItemsOne;
            lineItems = new List<LineItems>();
            if(new FileInfo("wwwroot/sources/menu/line item.json").Length == 0){
                lineItemsOne = new LineItems(PumpNum, LineNumber, Product,
                NetPrice, QuantityRequested);
                lineItems.Add(lineItemsOne);                
            } else{                         
                lineItems = await JsonSerializer.DeserializeAsync<List<LineItems>>(fs);         
                fs.SetLength(0);
                lineItemsOne = new LineItems(PumpNum, LineNumber, Product,
                NetPrice, QuantityRequested);
                lineItems.Add(lineItemsOne);
            }
            await JsonSerializer.SerializeAsync<List<LineItems>>(fs, lineItems);
            fs.Close();
        } 
    }
}