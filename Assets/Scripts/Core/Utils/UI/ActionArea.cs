using UnityEngine;
using System.Collections;

public class ActionArea : Slot {

    public UIStore uiStore;
    public TypeAction state;

    public enum TypeAction {
        Drop,
        Split
    }

    public override bool BlockSlot(DragItem dragItem) {
        Action(dragItem);
        return false;
    }

    public void Action(DragItem dragItem) {
        var stack = dragItem.currentItemStack;
        if (state == TypeAction.Drop) {
            uiStore.storeComponent.DropStack(stack);
        }
        if (state == TypeAction.Split) {
            uiStore.storeComponent.Split(stack);
        }
    }

}
