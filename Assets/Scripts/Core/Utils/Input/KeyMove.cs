using UnityEngine;
using System.Collections;

public class KeyMove : MonoBehaviour {

    private bool action = false;

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            GetComponent<Hero>().Action();
        }
        if (Input.GetKey("w") && Input.GetKey("d")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(1, 1).normalized);
            action = true;
            return;
        }
        if (Input.GetKey("w") && Input.GetKey("a")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(-1, 1).normalized);
            action = true;
            return;
        }
        if (Input.GetKey("s") && Input.GetKey("a")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(-1, -1).normalized);
            action = true;
            return;
        }
        if (Input.GetKey("s") && Input.GetKey("d")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(1, -1).normalized);
            action = true;
            return;
        }
        if (Input.GetKey("w")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(0,1).normalized);
            action = true;
            return;
        }
        if (Input.GetKey("s")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(0, -1).normalized);
            action = true;
            return;
        }
        if (Input.GetKey("a")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(-1, 0).normalized);
            action = true;
            return;
        }
        if (Input.GetKey("d")) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(1, 0).normalized);
            action = true;
            return;
        }
        if(!Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("d") && action) {
            action = false;
            GetComponent<CanMove>().Stop();
            return;
        }
    }
}
