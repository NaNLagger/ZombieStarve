using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanMove))]
public class CanAttack : MonoBehaviour {

    public int Damage;

    private CanMove moveComponent;
    private CanBeAttacked target;

    private void Awake() {
        moveComponent = GetComponent<CanMove>();
    }

    public void Attack(CanBeAttacked obj) {
        Debug.Log("I'm gonna attack you");
        if (Vector2.Distance(transform.position, obj.transform.position) > 1.5f) {
            moveComponent.Move(obj.transform.position, 1f);
            Reset();
            target = obj;
            moveComponent.OnDone += StartAttack;
            moveComponent.OnChangeTarget += Reset;
        }
        else {
            target = obj;
            StartAttack();
        }
    }

    public void StartAttack() {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
            animator.Play("Attack");
        else {
            EndAttack();
        }
    }

    public void EndAttack() {
        target.Hit(Damage);
        Reset();
    }

    public void Reset() {
        moveComponent.OnDone -= StartAttack;
        moveComponent.OnChangeTarget -= Reset;
        target = null;
    }
}