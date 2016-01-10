using UnityEngine;
using System.Collections;

public class SimpleObject : MonoBehaviour {

    [SerializeField] private int healthPoint;
    [SerializeField] private int maxHealthPoint;

    public Utils.EmptyEvent ChangeMaxHP;
    public Utils.EmptyEvent ChangeHP;
    public Utils.EmptyEvent OnDeath;

    public int MaxHP {
        get {
            return maxHealthPoint;
        }
        set {
            maxHealthPoint = value;
            if(ChangeMaxHP != null)
                ChangeMaxHP();
        }
    }

    public int HP {
        get {
            return healthPoint;
        }
        set {
            healthPoint = value;
            if (healthPoint <= 0)
                Death();
            if (ChangeHP != null)
                ChangeHP();
        }
    }

    private void Death() {
        if(OnDeath != null)
            OnDeath();
        Destroy(gameObject);
    }
}
