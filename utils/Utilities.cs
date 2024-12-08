namespace Utilities;
using XML_LWS2;
static public class DateUtilities{
    public static string reverseDate;
    public static string getReverseDate(){
       return DateTime.Now.ToString("yyMMddhhmmss");
    }

    
}

static public class OrderUtilities{
    public static string getFinalAmount(List<LineItems> lineItems){
        int tmp = 0;
        foreach(var lineItem in lineItems){
            tmp = tmp + Convert.ToInt32(lineItem.AmountAdjusted);
        }
        return Convert.ToString(tmp);
    }
}

static public class Utilities{
    public static async Task <string> getXML(){
            string fileText = await File.ReadAllTextAsync("wwwroot/sources/menu/xml.xml");
            return fileText;
        }
    public static string getURL(string eai_type){
        if(eai_type.Equals("LWS2")){
            return "DEV https://msk03-sbldev2.licard.com/siebel/app/eai_teboil/rus?SWEExtSource=WebService&SWEExtCmd=Execute&WSSOAP=1\n"
                + "TEST https://msk03-sbl2-tt1.licard.com/siebel/app/eai/enu?SWEExtSource=WebService&SWEExtCmd=Execute&WSSOAP=1\n"
                + "PRE https://msk03-sw3-pre.licard.com:9001/siebel/app/eai/enu?SWEExtSource=WebService&SWEExtCmd=Execute&WSSOAP=1";
        } else return "DEV https://msk03-sbldev2.licard.com:9001/siebel/app/eai_anon/rus?SWEExtSource=SecureWebService&SWEExtCmd=Execute\n"
        + "TEST https://msk03-sbl2-tt1.licard.com:9001/siebel/app/eai_anon/enu?SWEExtSource=SecureWebService&SWEExtCmd=Execute\n"
        + "PRE https://msk03-sw2-pre.licard.com:9001/siebel/app/eai_anon/rus?SWEExtSource=SecureWebService&SWEExtCmd=Execute";
    }

    public static string setColor(string type){
        string color;
        switch(type){
            case "Applet":
                color="#18b237";
            case "Application":
                color="#b29118";
            case "Business component":
                color="#1844b2";
            case "Business object":
                color="#1844b2";
            case "Business service":
                color="#7da60e";
            case "Import Object":
                color="#b29118";
            case "Integration object":
                color="#c60808";
            case "Job":
                color="#525216";
            case "Link":
                color="#133da9";
            case "Outbound REST Web Service":
                color="#c60808";
            case "Outbound  WSDL Web Service":
                color="#c60808";
            case "Pick List":
                color="#1844b2";
            case "Screen":
                color="#18b237";
            case "Task":
                color="#ad6a05";
            case "Table":
                color="#b29118";
            case "View":
                color="#18b237";
            case "Web Template":
                color="#b29118";
            case "Workflow Policy":
                color="#640764";
            case "Workflow Policy Program":
                color="#640764";
            case "Workflow Process":
                color="#3a0764";
            case "Product":
                color="#929213";
            default:
            break;
        }
        
        return color;
    }
}


