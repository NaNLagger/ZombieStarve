using UnityEngine;
using System.Collections.Generic;

public class CanDrop : MonoBehaviour {

    public List<Item> dropItems = new List<Item>();

    public void Drop() {
        foreach (Item item in dropItems) {
            Drop(item);
        }
        dropItems.Clear();
    }

    public void Drop(Item item) {
        ItemContainer drop = Instantiate(ResourcesLoader.LoadPref("ItemContainer")).GetComponent<ItemContainer>();
        drop.transform.position = transform.position;
        drop.SetItem(item);
    }
}
