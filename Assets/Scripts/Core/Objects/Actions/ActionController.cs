using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

    public Hero MainHero;

    private static ActionController instance;

    public static ActionController Instance {
        get {
            return instance;
        }
    }

    private void Awake() {
        instance = this;
    }

    public enum StatePriority {
        Move = 0,
        Collect = 1,
        Attack = 2
    }

}
