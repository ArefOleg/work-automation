using Microsoft.Data.Sqlite;

class BDConnection{
    public BDConnection(){
        using (var connection = new SqliteConnection("Data Source=task.db")){
            connection.Open();
        }
    }
}