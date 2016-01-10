using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SimpleObject)), RequireComponent(typeof(Collider2D))]
public class CanBeAttacked : MonoBehaviour {

    private SimpleObject baseScript;

    public Utils.EmptyEvent OnHit;

    private void Awake() {
        baseScript = GetComponent<SimpleObject>();
    }

    public void Hit(int damage) {
        baseScript.HP = baseScript.HP - damage;
        if (OnHit != null)
            OnHit();
    }
    
}
