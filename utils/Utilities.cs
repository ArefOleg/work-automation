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
}


