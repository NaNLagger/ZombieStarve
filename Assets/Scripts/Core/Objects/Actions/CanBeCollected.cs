using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SimpleObject))]
public class CanBeCollected : MonoBehaviour {

    private SimpleObject baseScript;
    public Item drop;

    private void Awake() {
        baseScript = GetComponent<SimpleObject>();
        if(drop == null) {
            ItemEntities.Init();
            drop = ItemEntities.items.Values.First();
        }
    }

    public Item Collect() {
        Debug.Log("Collect me");
        baseScript.HP = 0;
        return drop;
    }
}
