
using System.Xml.Serialization;
using XML_LWS2;

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