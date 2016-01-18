using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CanDrop)), RequireComponent(typeof(SimpleObject))]
public class CanStore : MonoBehaviour {

    public int Count = 1;
    public Dictionary<Item, int> storedItems = new Dictionary<Item, int>();

    public delegate void StoreEvent(Item item);
    public StoreEvent OnChangeItem;

    private void Awake() {
        GetComponent<SimpleObject>().OnDeath += Drop;
    }

    public bool Store(Item item) {
        if(storedItems.ContainsKey(item)) {
            storedItems[item]++;
            return true;
        } else {
            if(storedItems.Count < Count) {
                storedItems.Add(item, 1);
                return true;
            }
        }
        if (OnChangeItem != null)
            OnChangeItem(item);
        return false;
    }

    public void Drop() {
        var canDropComponent = GetComponent<CanDrop>();
        foreach(KeyValuePair<Item, int> item in storedItems) {
            for(int i=0; i < item.Value; i++) {
                canDropComponent.dropItems.Add(item.Key);
            }
        }
        canDropComponent.Drop();
    }

}
