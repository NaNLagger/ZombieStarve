using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanMove))]
public class JoystickInput : MonoBehaviour {

    public Joystick joystick;

    private bool action = false;

    private void Update() {
        if(joystick != null && joystick.action) {
            GetComponent<CanMove>().Move(transform.position + new Vector3(joystick.JoystickInput.x, joystick.JoystickInput.y));
            action = true;
        } 

        if(!joystick.action && action) {
            action = false;
            GetComponent<CanMove>().Stop();
        }
    }
}
