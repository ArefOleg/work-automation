namespace Utilities;
using XML_LWS2;
static public class DateUtilities{
    public static string reverseDate;
    public static string getReverseDate(){
       return DateTime.Now.ToString("yymmddhhmmss");
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
}
