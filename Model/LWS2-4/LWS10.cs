using System.Xml.Serialization;
using XML_Header;
namespace XML_LWS10;
public class JET_spcLWS10_spc_Input{
    [XmlElement("CardNumber", Namespace = "cusE")]
    public string CardNumber{get; set;}
    [XmlElement("Attrib1", Namespace = "cusE")]
    public string Attrib1{get; set;}
    [XmlElement("Attrib2", Namespace = "cusE")]
    public string Attrib2{get; set;}
    [XmlElement("Attrib3", Namespace = "cusE")]
    public string Attrib3{get; set;}
    [XmlElement("ProgramName", Namespace = "cusE")]
    public string ProgramName = "Teboil"{get; set;}
    [XmlElement("ActionName", Namespace = "cusE")]
    public string ActionName{get; set;}
    public JET_spcLWS10_spc_Input(){}
    public JET_spcLWS10_spc_Input(){
        Console.WriteLine("Введите номер карты");
        Console.WriteLine("DEV 7030040020000577\nTEST 7030040020015959\nPRE 7030040016723301");
        this.CardNumber = Console.ReadLine();
        Console.WriteLine("Введите тип действия. Самый частый это IssueQRAction");
        this.ActionName = Console.ReadLine();
        Console.WriteLine("Введите аттрибут1");
        Console.WriteLine("Для IssueQRAction это название для промо JET LWS10");
        Console.WriteLine("All Coffee Subscription\nFuel Subscription\nTeboil - Abonement2000_cars\nPower Offer for 1st refill");
        this.Attrib1 = Console.ReadLine();
        Console.WriteLine("Введите аттрибут2, для кофе и абонемента это количество купонов");
        this.Attrib2 = Console.ReadLine();
        Console.WriteLine("Введите аттрибут3");
        this.Attrib3 = Console.ReadLine();
    }

}
public class LWS10_Body{
    [XmlElement("JET_spcLWS10_spc-_spcAccrual_spcPoints_spcFor_spcAction_Input", Namespace = "cusE")]
    public JET_spcLWS10_spc_Input jET_SpcLWS10_Spc_Input;    
}
[XmlRoot("Envelope", Namespace = "soapenvE")]
public class LWS10{
    [XmlElement("Header", Namespace = "soapenvE")]   
    public Header_Eai_Anon_UserName_Token header_Eai_Anon_UserName_Token; 
    [XmlElement("Body", Namespace = "soapenvE")]
    public LWS10_Body lWS10_Body;
    
}

public static class generateLWS8XML{
    public static string generate(JET_spcLWS10_spc_Input jET_SpcLWS10_Spc_Input){
        LWS10_Body body = new LWS10_Body();
        body.jET_SpcLWS10_Spc_Input = jET_SpcLWS10_Spc_Input;
        LWS10 lWS10 = new LWS10();
        lWS10.lWS10_Body = body;
        Header_Eai_Anon_UserName_Token header = new Header_Eai_Anon_UserName_Token();
        lWS8.header_Eai_Anon_UserName_Token = header; 
        UsernameToken usernameToken = new UsernameToken();
        Security security = new Security();
        security.usernameToken = usernameToken;
        header.security = security;
        Task.WaitAll(LWS10Generator.generateXML(lWS10));
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
public static class LWS10Generator{
    public static async Task generateXML(LWS10 lWS10){
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("cus", "cusE");
        ns.Add("soapenv", "soapenvE");
        ns.Add("wsse", "wsseE");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LWS10));        
        using (FileStream fs = new FileStream("wwwroot/sources/menu/xml.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, lWS10, ns);            
        }
    }    
}