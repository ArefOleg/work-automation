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
        } else {
            var task = Task.Run(async () => await getAllLineItemsForOrderAsync());
            task.Wait();
            List<LineItems> lineItemsInner = task.Result;
            order.setFields(lineItemsInner);
            Task.WaitAll(clearJSONFile());
            foreach(LineItems lli in lineItemsInner){
                Console.WriteLine(lli.LineNumber);
            }
            ListOfJetorderaccrualredemptionrequest listOfJetorderaccrualredemptionrequest =
            new ListOfJetorderaccrualredemptionrequest();
            listOfJetorderaccrualredemptionrequest.order = order;
            JETLWS2OrderAccrualRedemption_1_Input jETLWS2OrderAccrualRedemption_1_Input = 
            new JETLWS2OrderAccrualRedemption_1_Input();
            jETLWS2OrderAccrualRedemption_1_Input.listOfJetorderaccrualredemptionrequest =
            listOfJetorderaccrualredemptionrequest;
            Body body = new Body();
            body.jETLWS2OrderAccrualRedemption_1_Input = 
            jETLWS2OrderAccrualRedemption_1_Input;
            Header_LWS header = new Header_LWS("TEBOIL_INT", "TEBOIL_INT", "None");
            LWS2 lWS2 = new LWS2();
            lWS2.body = body;
            lWS2.header = header;
            
            Task.WaitAll(LWSGenerator.generateXML(lWS2));
            var xmlTask = Task.Run(async () => await LWSGenerator.getXML());
            xmlTask.Wait();
            Message = xmlTask.Result.Replace("cusE", "http://siebel.com/CustomUI")
            .Replace("soapenvE", "http://schemas.xmlsoap.org/soap/envelope/")
            .Replace("jetE", "http://www.siebel.com/xml/JETOrderAccrualRedemptionRequest");
            
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
            //Написать сеттеры
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