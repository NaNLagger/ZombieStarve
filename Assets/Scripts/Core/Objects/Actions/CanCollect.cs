using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanMove)), RequireComponent(typeof(CanStore)), RequireComponent(typeof(CanDrop))]
public class CanCollect : MonoBehaviour {

    private CanMove moveComponent;
    private CanBeCollected target;
    private Animator animator;

    private void Awake() {
        moveComponent = GetComponent<CanMove>();
        animator = GetComponent<Animator>();
    }

    public void Collect(CanBeCollected obj) {
        if (target == obj && animator.GetInteger("State") == (int) ActionController.StatePriority.Collect)
            return;
        Reset();
        target = obj;
        Debug.Log("I'm gonna collect you");
        if (Vector2.Distance(transform.position, obj.transform.position) > 1f) {
            moveComponent.Move(obj.transform.position, 1f);
            moveComponent.OnDone += StartCollect;
            moveComponent.OnChangeTarget += Reset;
        } else {
            StartCollect();
        }
    }

    public void StartCollect() {
        animator.SetInteger("State", (int) ActionController.StatePriority.Collect);
    }

    public void EndCollect() {
        ItemStack item = target.Collect();
        if(!GetComponent<CanStore>().Store(item)) {
            GetComponent<CanDrop>().Drop(item);
        }
        animator.SetInteger("State", -1);
        Reset();
    }

    public void Reset() {
        Debug.Log("Reset");
        moveComponent.OnDone -= StartCollect;
        moveComponent.OnChangeTarget -= Reset;
        target = null;
    }
}
