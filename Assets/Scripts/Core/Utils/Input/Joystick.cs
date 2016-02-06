using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour {

    public float radius;
    public Vector2 JoystickInput;
    public bool action = false;

    public Canvas canvas;

    private Vector2 startPos;
    private Image joystickImage;
    private Touch TouchOnJoystick;
    private int ID;

    void Start() {
        startPos = gameObject.transform.position;
        joystickImage = GetComponent<Image>();
        
    }

    void Update() {
        
        if (Input.touchCount <= 0) {
            ID = -1;
        }
        else if (ID == -1 && Input.touchCount > 0) {
            for (var i = 0; i < Input.touchCount; ++i) {
                Vector2 temp = new Vector2(Input.touches[i].position.x, Input.touches[i].position.y) - startPos;
                if (temp.magnitude < radius * canvas.scaleFactor) {
                    if (Input.touches[i].phase == TouchPhase.Began) {
                        ID = Input.touches[i].fingerId;
                        action = true;
                        TouchOnJoystick = Input.touches[i];
                        Debug.Log(TouchOnJoystick.fingerId);
                    }
                }
            }
        }

        for (var i = 0; i < Input.touchCount; ++i) {
            if (ID == Input.touches[i].fingerId && action) {
                FollowFinger(Input.touches[i].position);
            }
        }

        for (var i = 0; i < Input.touchCount; ++i) {
            if (ID == Input.touches[i].fingerId && action) {
                if(Input.touches[i].phase == TouchPhase.Canceled || Input.touches[i].phase == TouchPhase.Ended) {
                    action = false;
                    if (joystickImage) {
                        joystickImage.CrossFadeAlpha(0.2f, .1f, true);
                    }
                    transform.position = startPos;
                    ID = -1;
                }
            }
        }

        UpdateInput();

#if UNITY_EDITOR
        /*//Mouse
        if (Input.GetMouseButtonDown(0)) {
            if (Mathf.Abs(Input.mousePosition.x - startPos.x) < radius &&
                    Mathf.Abs(Input.mousePosition.y - startPos.y) < radius) {
                FollowFinger(Input.mousePosition);
                action = true;
            }
        }

        if(Input.GetMouseButton(0) && action) {
            FollowFinger(Input.mousePosition);
        }
        
        if(Input.GetMouseButtonUp(0)) {
            gameObject.transform.position = startPos;
            action = false;
        }*/
#endif
    }

    private void FollowFinger(Vector2 mousePosition) {
        Vector2 temp = new Vector2(mousePosition.x, mousePosition.y) - startPos;
        
        if (temp.magnitude < radius * canvas.scaleFactor) {
            gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        } else {
            gameObject.transform.position = startPos + temp.normalized * radius * canvas.scaleFactor;
        }
        if (joystickImage) {
            joystickImage.CrossFadeAlpha(1, .2f, true);
        }
    }

    private void UpdateInput() {
        Vector2 temp = new Vector2(transform.position.x, transform.position.y) - startPos;
        JoystickInput = temp / radius;
    }

    void OnDrawGizmos() {
        /*if (!Application.isPlaying) {
            startPos = transform.position;
        }
        Gizmos.DrawWireSphere(startPos, radius * canvas.scaleFactor);*/
    }
}
