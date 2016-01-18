using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup))]
public class UIStore : MonoBehaviour {

    public CanStore storeComponent;

    private GridLayoutGroup layout;
    private Slot[] slots;

    private void Awake() {
        slots = transform.GetComponentsInChildren<Slot>();
        GenerateIcons();
        storeComponent.OnChangeItem += ChangeItem;
    }

    private void GenerateIcons() {
        int i = 0;
        foreach(KeyValuePair<Item, int> pair in storeComponent.storedItems) {
            DragItem dragItem = Instantiate(ResourcesLoader.LoadPref("ItemIcon")).GetComponent<DragItem>();
            dragItem.InitItem(pair.Key, pair.Value);
            if (i >= slots.Length)
                return;
            dragItem.SetSlot(slots[i]);
            i++;
        }
    }

    public void ChangeItem(Item item) {

    }

}
