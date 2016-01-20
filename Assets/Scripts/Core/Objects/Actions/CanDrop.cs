using UnityEngine;
using System.Collections.Generic;

public class CanDrop : MonoBehaviour {

    public List<ItemStack> dropStacks = new List<ItemStack>();

    public void Drop() {
        foreach (ItemStack stack in dropStacks) {
            Drop(stack);
        }
        dropStacks.Clear();
    }

    public void Drop(ItemStack stack) {
        ItemContainer drop = Instantiate(ResourcesLoader.LoadPref("ItemContainer")).GetComponent<ItemContainer>();
        drop.transform.position = transform.position;
        drop.SetItem(stack);
    }
}
