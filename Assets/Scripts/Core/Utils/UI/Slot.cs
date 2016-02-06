using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

    [SerializeField] private bool block = false;
    private UIStore storeController;
    private DragItem blockedItem;

    public bool isBlock
    {
        get {
            return block;
        }
    }

    public UIStore StoreCTRL
    {
        get
        {
            return storeController;
        }
    }

    public ItemStack BlockedStack {
        get {
            if (blockedItem != null)
                return blockedItem.currentItemStack;
            return null;
        }
    }

    public void SetParent(UIStore storeCtrl) {
        storeController = storeCtrl;
    }

    public virtual bool BlockSlot(DragItem dragItem) {
        if(isBlock) {
            blockedItem.currentItemStack.AddStack(dragItem.currentItemStack);
            return false;
        } else {
            blockedItem = dragItem;
            block = true;
            storeController.storeComponent.AddNewStack(dragItem.currentItemStack);
            return true;
        }
    }

    public void FreeSlot() {
        storeController.storeComponent.DeleteStack(blockedItem.currentItemStack);
        block = false;
        StoreCTRL.HideActionBar();
    }

    public void Restart() {
        if(blockedItem != null)
            blockedItem.Delete();
    }
}
