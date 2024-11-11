using System.Xml.Serialization;
using XML_Head;
namespace XML_S12;
public class S12_PersonalAddress{
    [XmlElement("Street", Namespace = "jetE")]
    public string Street{get; set;}
    [XmlElement("House", Namespace = "jetE")]
    public string House{get; set;}
    [XmlElement("Flat", Namespace = "jetE")]
    public string Flat{get; set;}
    [XmlElement("District", Namespace = "jetE")]
    public string District{get; set;}
    [XmlElement("Corps", Namespace = "jetE")]
    public string Corps{get; set;}
    [XmlElement("City", Namespace = "jetE")]
    public string City{get; set;}
    [XmlElement("Region", Namespace = "jetE")]
    public string Region{get; set;}
    [XmlElement("Index", Namespace = "jetE")]
    public string Index{get; set;}
    [XmlElement("Country", Namespace = "jetE")]
    public string Country{get; set;}
    [XmlElement("Type", Namespace = "jetE")]
    public string Type = "Домашний";
   /* [XmlElement("Id", Namespace = "jetE")]
    public string Id{get; set;}*/
    public S12_PersonalAddress(){}
}

public class ListOfPersonalAddress{
    [XmlElement("ListOfPersonalAddress", Namespace = "jetE")]
    public S12_PersonalAddress s12_PersonalAddress;
}
public class S12_Contact{
    [XmlElement("YlUserName", Namespace = "jetE")]
    public string YlUserName{get; set;}
    [XmlElement("YlPassword", Namespace = "jetE")]
    public string YlPassword{get; set;}
    [XmlElement("WorkPhone", Namespace = "jetE")]
    public string WorkPhone{get; set;}
    [XmlElement("SinceDate", Namespace = "jetE")]
    public string SinceDate{get; set;}
    [XmlElement("SecurityAnswer", Namespace = "jetE")]
    public string SecurityAnswer{get; set;}
    [XmlElement("NewCardNumber", Namespace = "jetE")]
    public string NewCardNumber{get; set;}
    [XmlElement("MiddleName", Namespace = "jetE")]
    public string MiddleName{get; set;}
    [XmlElement("LastName", Namespace = "jetE")]
    public string LastName{get; set;}
    [XmlElement("LanguageCode", Namespace = "jetE")]
    public string LanguageCode="RUS";
    [XmlElement("JETSignFlag", Namespace = "jetE")]
    public string JETSignFlag{get; set;}
    [XmlElement("JETRegistrationSource", Namespace = "jetE")]
    public string JETRegistrationSource="МП";
    [XmlElement("JETOSI", Namespace = "jetE")]
    public string JETOSI="WIN_10";
    [XmlElement("JETMarketingCampaignAgreement", Namespace = "jetE")]
    public string JETMarketingCampaignAgreement{get; set;}
    [XmlElement("FirstName", Namespace = "jetE")]
    public string FirstName{get; set;}
    [XmlElement("EmailAddress", Namespace = "jetE")]
    public string EmailAddress{get; set;}
    [XmlElement("CellularPhone", Namespace = "jetE")]
    public string CellularPhone{get; set;}
    [XmlElement("BirthDate", Namespace = "jetE")]
    public string BirthDate{get; set;}
    [XmlElement("Status", Namespace = "jetE")]
    public string Status{get; set;}
    [XmlElement("MF", Namespace = "jetE")]
    public string MF{get; set;}
    [XmlElement("ListOfPersonalAddress", Namespace = "jetE")]
    public ListOfPersonalAddress listOfPersonalAddress;
    public S12_Contact(){}
}

public class ListOfContact{    
    [XmlElement("Contact", Namespace = "jetE")]
    public S12_Contact s12_Contact;
}

public class JET_spcS12_spcCreate_spcAnketa{
    [XmlElement("ListOfContact", Namespace = "jetE")]
    public ListOfContact listOfContact;
    [XmlElement("SourceSystem", Namespace = "cusE")]
    public string SourceSystem = "МП";
}

public class JET_spcS12_Body{
    [XmlElement("JET_spcS12_spcCreate_spcAnketa_spcContact_spcWF_spc-_spcNL_Input", Namespace = "cusE")]
    public JET_spcS12_spcCreate_spcAnketa jET_SpcS12_SpcCreate_SpcAnketa;
}


[XmlRoot("Envelope", Namespace = "soapenvE")]
public class S12{
    [XmlElement("Header", Namespace = "soapenvE")]   
    public Header_Eai_Anon_UserName_Token header_Eai_Anon_UserName_Token; 
    [XmlElement("Body", Namespace = "soapenvE")]
    public JET_spcS12_Body jET_SpcS12_Body;
    
}



public static class generateS12XML{
    public static string generate(S12_Contact s12_Contact){
        ListOfContact listOfContact = new ListOfContact();
        listOfContact.s12_Contact = s12_Contact;
        JET_spcS12_spcCreate_spcAnketa jET_SpcS12_SpcCreate_SpcAnketa = new JET_spcS12_spcCreate_spcAnketa();
        jET_SpcS12_SpcCreate_SpcAnketa.listOfContact = listOfContact;
        JET_spcS12_Body body = new JET_spcS12_Body();
        body.jET_SpcS12_SpcCreate_SpcAnketa = jET_SpcS12_SpcCreate_SpcAnketa;
        S12 s12 = new S12();
        s12.jET_SpcS12_Body = body;        
        Header_Eai_Anon_UserName_Token header = new Header_Eai_Anon_UserName_Token();
        s12.header_Eai_Anon_UserName_Token = header; 
        UsernameToken usernameToken = new UsernameToken();
        Security security = new Security();
        security.usernameToken = usernameToken;
        header.security = security;
        Task.WaitAll(S12Generator.generateXML(s12));
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
        .Replace("xmlns:cus=\"cusE\"", "xmlns:cus=\"http://siebel.com/CustomUI\"")
        .Replace("jetE", "http://www.siebel.com/xml/JETOrderAccrualRedemptionRequest");
        
    }
}
public static class S12Generator{
    public static async Task generateXML(S12 s12){
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("cus", "cusE");
        ns.Add("soapenv", "soapenvE");
        ns.Add("wsse", "wsseE");
        ns.Add("jet", "jetE");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(S12));        
        using (FileStream fs = new FileStream("wwwroot/sources/menu/xml.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, s12, ns);            
        }
    }    
}