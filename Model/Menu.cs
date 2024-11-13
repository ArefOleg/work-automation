using System.Text.Json;
using System.Text.Json.Serialization;

namespace Work_Menu;

public class Menu{
    public List<MenuItem> menusStack;
    //public MenuItem menuItem;
    public Menu(){
        readJSONMenu("wwwroot/sources/menu/main menu.json");
    }

    public void readJSONMenu(string menuPath){
        string fileName = menuPath;
        string jsonStirng = File.ReadAllText(fileName);
        menusStack = new List<MenuItem>();
        menusStack = JsonSerializer.Deserialize<List<MenuItem>>(jsonStirng);        
    }
    public void eaiMenu(){
        menusStack.Clear();
        readJSONMenu("wwwroot/sources/menu/eai menu.json");
    }
    public void infoMenu(){
        menusStack.Clear();
        readJSONMenu("wwwroot/sources/menu/info menu.json");
    }

}

public class MenuItem{
    
    public string menuName{get;set;}   
    public string menuURL{get;set;}
    public string menuType{get;set;}
    public MenuItem(){}
    public MenuItem(string menuName, string menuURL, string menuType){
        this.menuName = menuName;
        this.menuURL = menuURL;
        this.menuType = menuType;
    }

}