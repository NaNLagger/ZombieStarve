using UnityEngine;
using System;

[Serializable]
public class Item {
    
    public string name;
    public string icon;
    public string sprite;

    public string Name {
        get {
            return name;
        }
    }

    public Sprite Icon {
        get {
            Sprite sprite = ResourcesLoader.LoadSprite("Icon/" + icon);
            if(sprite == null) {
                return ResourcesLoader.LoadSprite("Icon/no_icon");
            } else {
                return sprite;
            }
        }
    }

    public Sprite ObjSprite {
        get {
            Sprite sp = ResourcesLoader.LoadSprite(sprite);
            if (sp == null) {
                return ResourcesLoader.LoadSprite("Icon/no_icon");
            }
            else {
                return sp;
            }
        }
    }

    public Item(string name, string icon, string sprite) {
        this.name = name;
        this.icon = icon;
        this.sprite = sprite;
    }

    public override int GetHashCode() {
        if (name == null)
            return -1;
        return name.GetHashCode();
    }

    public override bool Equals(object obj) {
        if(obj is Item) {
            return ((Item)obj).name == this.name;
        }
        return false;
    }
}
