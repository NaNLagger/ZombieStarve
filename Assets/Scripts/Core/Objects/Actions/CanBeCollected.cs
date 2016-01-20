using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SimpleObject))]
public class CanBeCollected : MonoBehaviour {

    private SimpleObject baseScript;
    public ItemStack drop;

    private void Awake() {
        baseScript = GetComponent<SimpleObject>();
        if(drop == null) {
            ItemEntities.Init();
            drop = new ItemStack(ItemEntities.items.Values.First());
        }
    }

    public ItemStack Collect() {
        baseScript.HP = 0;
        return drop;
    }
}
