using System.Xml.Serialization;
//using XML_LWS4;
//using XML_LWS8;
//using XML_LWS10;
using XML_Head;
using XML_LWS2;

public abstract class XML_Generator{
    public TextWriter xml;
    public XmlSerializerNamespaces ns;
    public XmlSerializer xser;
    public string name;
    public XML_Generator(string name){
        this.name = name;
        this.xml = new StreamWriter(name + ".xml");
        this.ns = new XmlSerializerNamespaces();
        this.ns.Add("cus", "cusE");
        this.ns.Add("soapenv", "soapenvE");
    }
}
public class EAI_Anon_Generator : XML_Generator{
    public Header_Eai_Anon_UserName_Token header_Eai_Anon_UserName_Token;
    public UsernameToken usernameToken;
    public Security security;
    
    public EAI_Anon_Generator(string name) : base(name){
        this.header_Eai_Anon_UserName_Token = new Header_Eai_Anon_UserName_Token();
        this.usernameToken = new UsernameToken();
        this.security = new XML_Head.Security();
        this.header_Eai_Anon_UserName_Token.security = security;
        this.security.usernameToken = usernameToken;
        this.ns.Add("wsse", "wsseE");        
    }

    public void setNameSpaces(){
        xml.Close();
        string xmlText = File.ReadAllText(this.name + ".xml");
        xmlText = xmlText.Replace("cusE", "http://siebel.com/CustomUI")
        .Replace("soapenvE", "http://schemas.xmlsoap.org/soap/envelope/")
        .Replace("<wsse:Password>JET_INT", "<wsse:Password Type=\"wsse:PasswordText\">JET_INT")
        .Replace("xmlns:wsse=\"wsseE\"", "")
        .Replace("<wsse:Security>",
            "<wsse:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/07/secext\">")
        .Replace("<wsse:UsernameToken>",
            "<wsse:UsernameToken xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/07/utility\">");
        File.WriteAllText(name + ".xml", xmlText);
    }         
}