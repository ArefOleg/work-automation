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
    public void OnPost(string? PumpNum, string? LineNumber, string? Product, string? NetPrice,
    string? QuantityRequested)    
    {//нужно записывать json с продуктами в файл
    //после заполнения чека, формировать итоговое сообщение
        Message = "OnPost";
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
             FileMode.OpenOrCreate)){
            if(new FileInfo("wwwroot/sources/menu/line item.json").Length == 0){
                Console.WriteLine("проверка пройдена");
            }
        }
    /*    if(PumpNum is not null){
            using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
             FileMode.OpenOrCreate)){
                LineItems lineItemsOne;
                try{
                    lineItems = new List<LineItems>();                
                    lineItems = await JsonSerializer.DeserializeAsync<List<LineItems>>(fs);
                    byte[] buffer = Encoding.Default.GetBytes("");
                    await fs.WriteAsync(buffer, 0, buffer.Length);
                }
                catch{
                    lineItemsOne = new LineItems(PumpNum, LineNumber, Product,
                    NetPrice, QuantityRequested);
                    lineItems.Add(lineItemsOne);
                    await JsonSerializer.SerializeAsync<List<LineItems>>(fs, lineItems); 
                }
                    lineItemsOne = new LineItems(PumpNum, LineNumber, Product,
                    NetPrice, QuantityRequested);
                    lineItems.Add(lineItemsOne);
                    await JsonSerializer.SerializeAsync<List<LineItems>>(fs, lineItems); 
                                
             }            
        }
        */

    }
}