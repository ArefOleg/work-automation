using System.Text;
namespace SQL_Utils;
public static class SQL_Utilities{
    public static async Task <string> getDeclare(){
        string about = "";
        using (FileStream fs = new FileStream("wwwroot/sources/library/sql_scripts/Declare.txt",
         FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[fs.Length];
            await fs.ReadAsync(buffer, 0, buffer.Length);
            about = Encoding.Default.GetString(buffer);
        }
        return about;
    }

    public static async Task <string> getMounthReport(){
        string about = "";
        using (FileStream fs = new FileStream("wwwroot/sources/library/sql_scripts/task_scripts/call mounth report.txt",
         FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[fs.Length];
            await fs.ReadAsync(buffer, 0, buffer.Length);
            about = Encoding.Default.GetString(buffer);
        }
        return about;
    }

    public static async Task <string> getCreateTable(){
        string about = "";
        using (FileStream fs = new FileStream("wwwroot/sources/library/sql_scripts/create table.txt",
         FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[fs.Length];
            await fs.ReadAsync(buffer, 0, buffer.Length);
            about = Encoding.Default.GetString(buffer);
        }
        return about;
    }
    public static async Task <string> getCopyTable(){
        string about = "";
        using (FileStream fs = new FileStream("wwwroot/sources/library/sql_scripts/copy table.txt",
         FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[fs.Length];
            await fs.ReadAsync(buffer, 0, buffer.Length);
            about = Encoding.Default.GetString(buffer);
        }
        return about;
    }
}
