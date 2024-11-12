using System.Text;
namespace AboutEx;
public static class About{
    public static async Task <string> getADMInfo(){
        string about="";
        using (FileStream fs = new FileStream("wwwroot/sources/library/ADM.txt", FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[fs.Length];
            await fs.ReadAsync(buffer, 0, buffer.Length);
            about = Encoding.Default.GetString(buffer);
        }        
        return about;
    }
    public static async Task <string> getEnviromentInfo(){
        string about = "";
        using (FileStream fs = new FileStream("wwwroot/sources/library/enviroment.txt",
         FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[fs.Length];
            await fs.ReadAsync(buffer, 0, buffer.Length);
            about = Encoding.Default.GetString(buffer);
        }   
        return about;
    }

}