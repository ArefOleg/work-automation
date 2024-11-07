using System.Xml.Serialization;
using XML_Head;
namespace XML_LWS8;
public class JETLWS8GetTransactions_1_Input{
    [XmlElement("CardNumber", Namespace = "cusE")]
    public string CardNumber{get;set;}
    [XmlElement("PageSize", Namespace = "cusE")]
    public string PageSize{get;set;}
    [XmlElement("Source", Namespace = "cusE")]
    public string Source = "МП";
    [XmlElement("StartRowNum", Namespace = "cusE")]
    public string StartRowNum = "1";
    [XmlElement("StartDate", Namespace = "cusE")]
    public string StartDate {get;set;}
    [XmlElement("EndDate", Namespace = "cusE")]
    public string EndDate{get;set;}
    public JETLWS8GetTransactions_1_Input(){}

}
public class JETLWS8Body{
    [XmlElement("JETLWS8GetTransactions_1_Input", Namespace = "cusE")]
    public JETLWS8GetTransactions_1_Input jETLWS8GetTransactions_1_Input;    
}
[XmlRoot("Envelope", Namespace = "soapenvE")]
public class LWS8{
    [XmlElement("Header", Namespace = "soapenvE")]   
    public Header_Eai_Anon_UserName_Token header_Eai_Anon_UserName_Token; 
    [XmlElement("Body", Namespace = "soapenvE")]
    public JETLWS8Body jETLWS8Body;
    
}

public static class generateLWS8XML{
    public static string generate(JETLWS8GetTransactions_1_Input jETLWS8GetTransactions_1_Input){
        JETLWS8Body body = new JETLWS8Body();
        body.jETLWS8GetTransactions_1_Input = jETLWS8GetTransactions_1_Input;
        LWS8 lWS8 = new LWS8();
        lWS8.jETLWS8Body = body;
        Header_Eai_Anon_UserName_Token header = new Header_Eai_Anon_UserName_Token();
        lWS8.header_Eai_Anon_UserName_Token = header; 
        UsernameToken usernameToken = new UsernameToken();
        Security security = new Security();
        security.usernameToken = usernameToken;
        header.security = security;
        Task.WaitAll(LWS8Generator.generateXML(lWS8));
        var xmlTask = Task.Run(async () => await Utilities.Utilities.getXML());
        xmlTask.Wait();            
        return xmlTask.Result
        .Replace("soapenvE", "http://schemas.xmlsoap.org/soap/envelope/")
        .Replace("<wsse:Password>JET_INT", "<wsse:Password Type=\"wsse:PasswordText\">JET_INT")
        .Replace("xmlns:wsse=\"wsseE\"", "")
        .Replace("<wsse:Security>",
            "<wsse:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/07/secext\">")
        .Replace("<wsse:UsernameToken>",
            "<wsse:UsernameToken xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/07/utility\">")
        .Replace("xmlns:cus=\"cusE\"", "xmlns:cus=\"http://siebel.com/CustomUI\"");
        
    }
}
public static class LWS8Generator{
    public static async Task generateXML(LWS8 lWS8){
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("cus", "cusE");
        ns.Add("soapenv", "soapenvE");
        ns.Add("wsse", "wsseE");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LWS8));        
        using (FileStream fs = new FileStream("wwwroot/sources/menu/xml.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, lWS8, ns);            
        }
    }    
}