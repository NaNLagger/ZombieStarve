using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SimpleObject)), RequireComponent(typeof(CanBeAttacked))]
public class TestDog : MonoBehaviour {

    void Awake() {
        GetComponent<SimpleObject>().OnDeath += Drop;
    }

    public void Drop() {
        ItemContainer drop = Instantiate(ResourcesLoader.LoadPref("ItemContainer")).GetComponent<ItemContainer>();
        drop.transform.position = transform.position;
        drop.SetItem(ItemEntities.items["Wood"]);
    }
}
