using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(CanCollect))]
public class Hero : MonoBehaviour {

    private CanCollect collectComponent;
    private CanAttack attackComponent;
    private CanOpen openComponent;

    private void Awake() {
        collectComponent = GetComponent<CanCollect>();
        attackComponent = GetComponent<CanAttack>();
        openComponent = GetComponent<CanOpen>();
    }

    public bool Action(SimpleObject obj) {
        if (obj.gameObject == gameObject)
            return false;
        Component component = obj.gameObject.GetComponent("CanBeCollected");
        if (component != null) {
            collectComponent.Action(obj);
            return true;
        }
        component = obj.gameObject.GetComponent("CanBeAttacked");
        if (component != null) {
            attackComponent.Attack((CanBeAttacked)component);
            return true;
        }
        component = obj.gameObject.GetComponent("CanStore");
        if (component != null) {
            openComponent.Action(obj);
            return true;
        }
        return false;
    }
    
    public void Action() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 4f);
        int i = 0;
        Collider2D[] sortedColliders = hitColliders.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToArray();
        while (i < sortedColliders.Length) {
            if (sortedColliders[i].GetComponent<SimpleObject>() != null)
                if (Action(sortedColliders[i].GetComponent<SimpleObject>()))
                    return;
            i++;
        }
    }
}
