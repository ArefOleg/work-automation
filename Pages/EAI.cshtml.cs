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
using XML_LWS10;
using Utilities;
using S21_XML;
namespace work_automation.Pages;

public class EaiModel : PageModel
{   public Menu mainMenu;
    public string Action{get; set;}
    public string integrationService{get; set;} 
    public List<LineItems> lineItems;
    public LineItems lineItem{get; set;}
    public Order order{get; set;}
    public JET_S21_Get_Client_Info_Input jET_S21_Get_Client_Info_Input{get; set;}
    public JET_spcLWS10_spc_Input jET_SpcLWS10_Spc_Input{get; set;}
    public JETLWS4OrderCancel_Input jETLWS4OrderCancel_Input{get; set;}
    public JETLWS8GetTransactions_1_Input jETLWS8GetTransactions_1_Input{get; set;}
    public S12_PersonalAddress s12_PersonalAddress{get; set;}
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
    JETLWS8GetTransactions_1_Input? jETLWS8GetTransactions_1_Input,
    JET_spcLWS10_spc_Input? jET_SpcLWS10_Spc_Input,
    JET_S21_Get_Client_Info_Input? jET_S21_Get_Client_Info_Input,
    S12_PersonalAddress? s12_PersonalAddress)    
    {   integrationService = service;
        Task.WaitAll(clearXML());
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
            URL = Utilities.Utilities.getURL("LWS2");
            

        } else if(Action.Equals("LWS4")){
            Message = generateLWS4XML.generate(jETLWS4OrderCancel_Input);
            service = "LWS4";
            URL = Utilities.Utilities.getURL("LWS2");
        } else if(Action.Equals("LWS8")){
            Message = generateLWS8XML.generate(jETLWS8GetTransactions_1_Input);
            service = "LWS8";
            URL = Utilities.Utilities.getURL("LWS8");
        } else if(Action.Equals("LWS10")){
            Message = generateLWS10XML.generate(jET_SpcLWS10_Spc_Input);
            service = "LWS10";
            URL = Utilities.Utilities.getURL("LWS10");
        }
        else if(Action.Equals("S21")){
            Message = generateS21XML.generate(jET_S21_Get_Client_Info_Input);
            service = "S21";
            URL = Utilities.Utilities.getURL("S21");
        }
        else if(Action.Equals("Address")){
            lineItem.setAmountAdjusted();
            Task.WaitAll(fillAddress(s12_PersonalAddress));
            service = "Address";
        }
        else if(Action.Equals("S12")){

        }
         
    }
    public async Task fillAddress(S12_PersonalAddress s12_PersonalAddress){
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
             FileMode.OpenOrCreate)){
            fs.SetLength(0);
            await JsonSerializer.SerializeAsync<S12_PersonalAddress>(fs, s12_PersonalAddress);
            fs.Close();
        } 
    }

    public async Task<S12_PersonalAddress> getAddress(){
        S12_PersonalAddress S12AP;
        using (FileStream fs = new FileStream("wwwroot/sources/menu/line item.json",
            FileMode.OpenOrCreate)){
            S12AP = await JsonSerializer.DeserializeAsync<S12_PersonalAddress>(fs);                        
        }
        return S12AP;
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