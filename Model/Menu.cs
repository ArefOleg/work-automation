using System.Text.Json;
using System.Text.Json.Serialization;

namespace Work_Menu;

public class Menu{
    public List<MenuItem> menus;
    public List<MenuItem> sortedMenus;
    //public MenuItem menuItem;
    public Menu(){
        readJSONMenu();
        var sort = from m in menus where m.menuType.Equals("mainMenu") select m;
        foreach(MenuItem menu in sort) sortedMenus.Add(menu);
    }

    public void readJSONMenu(){
        string fileName = "wwwroot/sources/menu/main menu.json";
        string jsonStirng = File.ReadAllText(fileName);
        menus = new List<MenuItem>();
        sortedMenus = new List<MenuItem>();
        menus = JsonSerializer.Deserialize<List<MenuItem>>(jsonStirng);        
    }
    public void eaiMenu(){
        sortedMenus.Clear();
        var sort = from m in menus where m.menuType.Equals("eaiMenu") select m;
        foreach(MenuItem menu in sort) sortedMenus.Add(menu);
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