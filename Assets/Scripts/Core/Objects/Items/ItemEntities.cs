using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemEntities {

    public static Dictionary<string, Item> items = new Dictionary<string, Item>();
    public static bool isInit = false;

    public static void Init() {
        if (isInit)
            return;
        isInit = true;
        items = new Dictionary<string, Item>();
        //items.Add("Wood_test", new Item("Wood", "Icon/no_icon", "Icon/no_icon"));
        LoadItems();
    }

    private static void LoadItems() {
        object[] objects = ResourcesLoader.LoadAllObjectInDirectory("Entities/Items");
        foreach(Item item in objects) {
            items.Add(item.Name, item);
        }
    }

    public static void Update() {
        items = new Dictionary<string, Item>();
        LoadItems();
    }
}
