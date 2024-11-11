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