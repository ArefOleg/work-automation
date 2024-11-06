using System.Xml.Serialization;
using XML_Header;
using Utilities;
namespace XML_LWS2;
public class LineItems{
    [XmlElement("PumpNum", Namespace = "jetE")]
    public string PumpNum { get; set; }
    [XmlElement("AmountAdjusted", Namespace = "jetE")]
    public string AmountAdjusted { get; set; }
    [XmlElement("LineNumber", Namespace = "jetE")]
    public string LineNumber { get; set; }
    [XmlElement("NetPrice", Namespace = "jetE")]
    public string NetPrice { get; set; }
    [XmlElement("Product", Namespace = "jetE")]
    public string Product { get; set; }
    [XmlElement("QuantityRequested", Namespace = "jetE")]
    public string QuantityRequested { get; set; }
    public LineItems(string PumpNum, string LineNumber, string Product,
    string NetPrice, string QuantityRequested){
        //Console.WriteLine("Введите порядковый номер колонки");
        this.PumpNum = PumpNum;
        //Console.WriteLine("Введите номер позиции");
        this.LineNumber = LineNumber;
        //Console.WriteLine("Введите продукт \nТопливо 51ff4750-d485-11e5-8102-00155d0d2201 \n" + 
        //"Топливо + e0d8dfdb-958f-11ec-8470-000c290469ea \n" +
        //"НТУ 079b7231-d4ec-11eb-845c-000c290469ea");        
        this.Product = Product;        
        //Console.WriteLine("Введите цену за 1 шт товара");        
        this.NetPrice = NetPrice;        
        //Console.WriteLine("Введите количество");
        this.QuantityRequested = QuantityRequested;
        this.AmountAdjusted = Convert.ToString(Convert.ToInt32(this.QuantityRequested) * Convert.ToInt32(this.NetPrice));
        //Console.WriteLine($"Итоговая цена за всю позицию: {this.AmountAdjusted}");                
    }
    public void setAmountAdjusted(){
        this.AmountAdjusted = Convert.ToString(Convert.ToInt32(this.QuantityRequested) * Convert.ToInt32(this.NetPrice));
    }
    public LineItems(){}
}

public class Order{
    
    [XmlElement("Revision", Namespace = "jetE")]
    public string Revision = "1";
    [XmlElement("OrderNumber", Namespace = "jetE")]
    public string OrderNumber { get; set; }
    [XmlElement("CardNumber", Namespace = "jetE")]
    public string CardNumber { get; set; }
    [XmlElement("OrderDate", Namespace = "jetE")]
    public string OrderDate { get; set; }
    [XmlElement("OrderAmount", Namespace = "jetE")]
    public string OrderAmount { get; set; }
    [XmlElement("OrderType", Namespace = "jetE")]
    public string OrderType { get; set; }
    [XmlElement("OrderCurrency", Namespace = "jetE")]
    public string OrderCurrency = "643";
    [XmlElement("Attrib1", Namespace = "jetE")]
    public string Attrib1 { get; set; }
    [XmlElement("RRN", Namespace = "jetE")]
    public string RRN { get; set; }
    [XmlElement("DiscountCUR", Namespace = "jetE")]
    public string DiscountCUR { get; set; }
    [XmlElement("TerminalId", Namespace = "jetE")]
    public string TerminalId { get; set; }
    [XmlElement("AcquiringId", Namespace = "jetE")]
    public string AcquiringId { get; set; }
    [XmlElement("CardAcceptorId", Namespace = "jetE")]
    public string CardAcceptorId = "CardAcceptorId";
    [XmlElement("LineItems", Namespace = "jetE")]
    public List<LineItems> lineItems = new List<LineItems>();
    public Order(){}
    public void setFields(List<LineItems> lineItems){
        this.OrderNumber = "OV" + DateTime.Now.ToString("h:mm:ss dd");
        this.OrderDate = Utilities.DateUtilities.getReverseDate(); 
        this.RRN = this.OrderNumber;
        this.lineItems = lineItems;
    }
    public Order(string lineItemAmount, string CardNumber, string OrderType,
    string Attrib1, string DiscountCUR, string TerminalId, string AcquiringId){        
        this.OrderNumber = "OV" + DateTime.Now.ToString("h:mm:ss dd");
        //Console.WriteLine("Введите номер карты");
        //Console.WriteLine("DEV 7030040020000577\nTEST 7030040020015959\nPRE 7030040016723301");
        this.CardNumber = CardNumber;
        this.OrderDate = Utilities.DateUtilities.getReverseDate(); 
        //Console.WriteLine("Введите тип чека, например QR-code");
        this.OrderType = OrderType;
        Console.WriteLine("Введите Аттрибут 1");
        this.Attrib1 = Attrib1;
        Console.WriteLine("Введите скидку");
        this.DiscountCUR = DiscountCUR;
        Console.WriteLine("Введите терминал");
        Console.WriteLine("DEV 0000549309031095\nTEST 0001187072026239\nPRE 0000749595042549");
        this.TerminalId = TerminalId;
        Console.WriteLine("Введите идентификатор экварианговой сети");
        this.AcquiringId = AcquiringId;
        this.RRN = this.OrderNumber;
        this.OrderAmount = lineItemAmount;
    }
}


public class ListOfJetorderaccrualredemptionrequest{
    [XmlElement("Order", Namespace = "jetE")]
    public Order order;
}

public class JETLWS2OrderAccrualRedemption_1_Input{
    [XmlElement("ListOfJetorderaccrualredemptionrequest", Namespace = "jetE")]
    public ListOfJetorderaccrualredemptionrequest listOfJetorderaccrualredemptionrequest;
}

public class Body{
    [XmlElement("JETLWS2OrderAccrualRedemption_1_Input", Namespace = "cusE")]
    public JETLWS2OrderAccrualRedemption_1_Input jETLWS2OrderAccrualRedemption_1_Input;    
}

[XmlRoot("Envelope", Namespace = "soapenvE")]
public class LWS2{
        [XmlElement("Header", Namespace = "soapenvE")]
        public Header_LWS header;
        [XmlElement("Body", Namespace = "soapenvE")]
        public Body body;        

    }

public static class generateLWS2XML{
    public static string generate(Order order){
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
            System.Xml.Linq.XDocument xdoc = System.Xml.Linq.XDocument.Load("wwwroot/sources/menu/xml.xml");
            string xml = xdoc.ToString();
            return xml.Replace("cusE", "http://siebel.com/CustomUI")
            .Replace("soapenvE", "http://schemas.xmlsoap.org/soap/envelope/")
            .Replace("jetE", "http://www.siebel.com/xml/JETOrderAccrualRedemptionRequest");
    }
}
public static class LWSGenerator{
    public static async Task generateXML(LWS2 lws2){
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("cus", "cusE");
        ns.Add("soapenv", "soapenvE");
        ns.Add("jet", "jetE");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LWS2));        
        using (FileStream fs = new FileStream("wwwroot/sources/menu/xml.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, lws2, ns);            
        }
    }

    public static async Task <string> getXML(){
        string fileText = await File.ReadAllTextAsync("wwwroot/sources/menu/xml.xml");
        return fileText;
    }
}