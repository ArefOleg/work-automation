using XML_Header;
using System.Xml.Serialization;
using XML_LWS4;
namespace XML_LWS4
{
    public class JETLWS4OrderCancel_Input
    {
        [XmlElement("CardNumber", Namespace = "cusE")]
        public string cardNumber { get; set; }
        [XmlElement("TerminalId", Namespace = "cusE")]
        public string terminalId {get; set; }
        [XmlElement("CardAcceptorId", Namespace = "cusE")]
        public string cardAcceptorId = "CardAcceptorId";
        [XmlElement("AcquiringId", Namespace = "cusE")]
        public string acquiringId = "AcquiringId";
        [XmlElement("RRN", Namespace = "cusE")]
        public string RRN;
        public JETLWS4OrderCancel_Input(){}
    }
    public class Body{
        [XmlElement("JETLWS4OrderCancel_Input", Namespace = "cusE")]
        public JETLWS4OrderCancel_Input jETLWS4OrderCancel_Input;
    }
    [XmlRoot("Envelope", Namespace = "soapenvE")]
    public class LWS4{
        [XmlElement("Header", Namespace = "soapenvE")]
        public Header_LWS header;
        [XmlElement("Body", Namespace = "soapenvE")]
        public Body body;
        

    }
}

public static class generateLWS4XML{
    public static string generate(JETLWS4OrderCancel_Input jETLWS4OrderCancel_Input){
        Body body = new Body();
        body.jETLWS4OrderCancel_Input = jETLWS4OrderCancel_Input;
        LWS4 lWS4 = new LWS4();
        lWS4.body = body;
        Header_LWS header = new Header_LWS("TEBOIL_INT", "TEBOIL_INT", "None");
        lWS4.header = header;                
        Task.WaitAll(LWS4Generator.generateXML(lWS4));
        var xmlTask = Task.Run(async () => await Utilities.Utilities.getXML());
        xmlTask.Wait();            
        return xmlTask.Result.Replace("cusE", "http://siebel.com/CustomUI")
        .Replace("soapenvE", "http://schemas.xmlsoap.org/soap/envelope/");
    }
}
public static class LWS4Generator{
    public static async Task generateXML(LWS4 lWS4){
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("cus", "cusE");
        ns.Add("soapenv", "soapenvE");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LWS4));        
        using (FileStream fs = new FileStream("wwwroot/sources/menu/xml.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, lWS4, ns);            
        }
    }    
}