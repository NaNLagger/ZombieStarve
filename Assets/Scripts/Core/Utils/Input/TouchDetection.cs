using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class TouchDetection : MonoBehaviour {

    private GameObject parent;

    private void Awake() {
        parent = transform.parent.gameObject;
    }

    private void OnMouseUpAsButton() {
        Debug.Log("Touch");
        parent.SendMessage("Action");
    }
}
