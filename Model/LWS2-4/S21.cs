using System.Xml.Serialization;
using XML_Head;
namespace S21_XML;

public class JET_S21_Get_Client_Info_Input{
    [XmlElement("LoginLK", Namespace = "cusE")]
    public string LoginLK{get; set;}
    [XmlElement("ProgramName", Namespace = "cusE")]
    public string ProgramName = "Teboil";
    [XmlElement("Source", Namespace = "cusE")]
    public string Source = "МП";
    public JET_S21_Get_Client_Info_Input(){}
}
public class S21_Body{
    [XmlElement("JET_S21_Get_Client_Info_Input", Namespace = "cusE")]
    public JET_S21_Get_Client_Info_Input jET_S21_Get_Client_Info_Input;    
}
[XmlRoot("Envelope", Namespace = "soapenvE")]
public class S21{
    [XmlElement("Header", Namespace = "soapenvE")]   
    public Header_Eai_Anon_UserName_Token header_Eai_Anon_UserName_Token; 
    [XmlElement("Body", Namespace = "soapenvE")]
    public S21_Body s21_Body;
    
}


public static class generateS21XML{
    public static string generate(JET_S21_Get_Client_Info_Input jET_S21_Get_Client_Info_Input){
        S21_Body body = new S21_Body();
        body.jET_S21_Get_Client_Info_Input = jET_S21_Get_Client_Info_Input;
        S21 s21 = new S21();
        s21.s21_Body = body;        
        Header_Eai_Anon_UserName_Token header = new Header_Eai_Anon_UserName_Token();
        s21.header_Eai_Anon_UserName_Token = header; 
        UsernameToken usernameToken = new UsernameToken();
        Security security = new Security();
        security.usernameToken = usernameToken;
        header.security = security;
        Task.WaitAll(S21Generator.generateXML(s21));
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
public static class S21Generator{
    public static async Task generateXML(S21 s21){
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("cus", "cusE");
        ns.Add("soapenv", "soapenvE");
        ns.Add("wsse", "wsseE");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(S21));        
        using (FileStream fs = new FileStream("wwwroot/sources/menu/xml.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, s21, ns);            
        }
    }    
}