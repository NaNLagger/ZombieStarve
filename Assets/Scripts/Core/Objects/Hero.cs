using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CanCollect))]
public class Hero : MonoBehaviour {

    private CanCollect collectComponent;
    private CanAttack attackComponent;

    private void Awake() {
        collectComponent = GetComponent<CanCollect>();
        attackComponent = GetComponent<CanAttack>();
    }

    public bool Action(SimpleObject obj) {
        Component component = obj.gameObject.GetComponent("CanBeCollected");
        if (component != null) {
            collectComponent.Collect((CanBeCollected) component);
            return true;
        }
        component = obj.gameObject.GetComponent("CanBeAttacked");
        if (component != null) {
            attackComponent.Attack((CanBeAttacked)component);
            return true;
        }
        return false;
    }
    
    public void Action() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 4f);
        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[i].GetComponent<SimpleObject>() != null)
                if (Action(hitColliders[i].GetComponent<SimpleObject>()))
                    return;
            i++;
        }
    }

}
