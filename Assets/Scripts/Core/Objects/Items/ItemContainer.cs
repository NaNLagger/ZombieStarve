using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanBeCollected)), RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(UpdateOrder))]
public class ItemContainer : MonoBehaviour {

    private Item currentItem;

    public void Start() {
        if(currentItem != null) {
            GetComponent<SpriteRenderer>().sprite = currentItem.ObjSprite;
            GetComponent<CanBeCollected>().drop = currentItem;
        }
    }

    public void SetItem(Item item) {
        GetComponent<SpriteRenderer>().sprite = item.ObjSprite;
        GetComponent<CanBeCollected>().drop = item;
    }
}
