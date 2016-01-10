using UnityEngine;
using System.Collections;

public class UpdateOrder : MonoBehaviour {

    private void Start() {
        SetOrder();
    }

    public void SetOrder() {
        ChildOrderUpdate[] childs = transform.GetComponentsInChildren<ChildOrderUpdate>();
        if(GetComponent<SpriteRenderer>() != null) {
            GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * -10) * 20;
        }
        foreach (ChildOrderUpdate child in childs)
            child.UpdateOrder((int)(transform.position.y * -10) * 20);
    }
}
