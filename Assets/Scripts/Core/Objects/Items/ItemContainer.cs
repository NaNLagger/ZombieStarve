using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanBeCollected)), RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(UpdateOrder))]
public class ItemContainer : MonoBehaviour {

    private ItemStack currentStack;

    public void Start() {
        if(currentStack != null) {
            GetComponent<SpriteRenderer>().sprite = currentStack.TypeItem.ObjSprite;
            GetComponent<CanBeCollected>().drop = currentStack;
        }
    }

    public void SetItem(ItemStack stack) {
        currentStack = stack;
        GetComponent<SpriteRenderer>().sprite = currentStack.TypeItem.ObjSprite;
        GetComponent<CanBeCollected>().drop = currentStack;
    }
}
