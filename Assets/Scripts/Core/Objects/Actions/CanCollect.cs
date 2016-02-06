using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanMove)), RequireComponent(typeof(CanStore)), RequireComponent(typeof(CanDrop))]
public class CanCollect : CanAction {

    protected override void Awake() {
        base.Awake();
        state = ActionController.StatePriority.Collect;
    }

    public override void EndAction() {
        
        ItemStack item = target.gameObject.GetComponent<CanBeCollected>().Collect();
        if(!GetComponent<CanStore>().Store(item)) {
            GetComponent<CanDrop>().Drop(item);
        }
        base.EndAction();
    }
}
