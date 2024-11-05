using System.Xml.Serialization;
namespace XML_Header;

public class Header_Eai_Anon_UserName_Token{
    [XmlElement("Security", Namespace = "wsseE")]
    public Security security;
    public Header_Eai_Anon_UserName_Token(){
    }    
}

public class Security{
    [XmlElement("UsernameToken", Namespace = "wsseE")]
    public UsernameToken usernameToken;    
}

public class UsernameToken{
        [XmlElement("Username", Namespace = "wsseE")]
        public string Username = "JET_INT";
        [XmlElement("Password")]
        public string Password = "JET_INT";
    }

public class Header_LWS{
    [XmlElement("UsernameToken", Namespace = "http://siebel.com/webservices")]
    public string usernameToken;
    [XmlElement("PasswordText", Namespace = "http://siebel.com/webservices")]
    public string passwordText;
    [XmlElement("SessionType", Namespace = "http://siebel.com/webservices")]
    public string sessionType;

    public Header_LWS(){}
    public Header_LWS(string usernameToken, string passwordText, string sessionType){
        this.usernameToken = usernameToken;
        this.passwordText = passwordText;
        this.sessionType = sessionType;
    }


    
}