using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GridLayoutGroup))]
public class UIStore : MonoBehaviour {

    public CanStore storeComponent;
    
    private Slot[] slots;

    private void Awake() {
        slots = transform.GetComponentsInChildren<Slot>();
        foreach (Slot slot in slots)
            slot.SetParent(this);
        GenerateIcons();
        storeComponent.OnInsertStack += InsertDragItem;
    }

    private void GenerateIcons() {
        foreach(ItemStack stack in storeComponent.storedItems) {
            AddStack(stack);
        }
    }

    private void AddStack(ItemStack stack) {
        Slot freeSlot = slots.FirstOrDefault(x => !x.isBlock);
        DragItem dragItem = Instantiate(ResourcesLoader.LoadPref("ItemIcon")).GetComponent<DragItem>();
        dragItem.InitItem(stack);
        dragItem.SetSlot(freeSlot);
    }

    public void InsertDragItem(ItemStack stack) {
        if(slots.FirstOrDefault(x => x.BlockedStack == stack) == null) {
            AddStack(stack);
        }
    }

}
