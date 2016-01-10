using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class ItemCreator : MonoBehaviour {

    public Item addItem = new Item("name", "icon", "sprite");
    public Item editItem;

    void Awake() {
        ItemEntities.Init();
        editItem = ItemEntities.items.Values.First();
    }

}
