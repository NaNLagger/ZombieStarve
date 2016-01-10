using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanMove))]
public class MouseMove : MonoBehaviour {

    void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            GetComponent<CanMove>().Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
