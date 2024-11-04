using System.Xml.Serialization;
using XML_Head;
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