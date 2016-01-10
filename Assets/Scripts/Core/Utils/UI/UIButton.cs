using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image)), ExecuteInEditMode]
public class UIButton : MonoBehaviour {

    [Serializable]
    public enum TypeInteraction {
        ColorTint,
        SpriteSwap
    }

    public bool Interactable = true;
    public TypeInteraction Transition = TypeInteraction.ColorTint;
    public Graphic TargetGraphic = null;
    public Color NormalColor = Color.white;
    public Color PressedColor = Color.gray;
    public float Duration = 0.1f;

    public UnityEvent OnDown = new UnityEvent();
    public UnityEvent OnHold = new UnityEvent();
    public UnityEvent OnUp = new UnityEvent();

    private Touch followTouch;
    private RectTransform rectTransform;

    public void OnEnable() {
        if (TargetGraphic == null) {
            TargetGraphic = GetComponent<Image>();
        }
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update() {
        
        if(Interactable) {
            foreach(Touch touch in Input.touches) {
                if(rectTransform.rect.Contains((Vector3) touch.position - rectTransform.position) && touch.phase == TouchPhase.Began) {
                    followTouch = touch;
                    OnPointerDown();
                    
                }
            }

            foreach (Touch touch in Input.touches) {
                if (followTouch.fingerId == touch.fingerId && rectTransform.rect.Contains((Vector3)touch.position - rectTransform.position)) {
                    if (OnHold != null)
                        OnHold.Invoke();
                }
            }

            foreach (Touch touch in Input.touches) {
                if(followTouch.fingerId == touch.fingerId && 
                    (touch.phase == TouchPhase.Ended 
                    || touch.phase == TouchPhase.Canceled 
                    || !rectTransform.rect.Contains((Vector3) touch.position - rectTransform.position))) {
                    OnPointerUp();
                }
            }
        }

    }

    public void OnPointerDown() {
        TargetGraphic.CrossFadeColor(PressedColor, Duration, true, true);
        if(OnDown != null)
            OnDown.Invoke();
    }

    public void OnPointerUp() {
        TargetGraphic.CrossFadeColor(NormalColor, Duration, true, true);
        if (OnUp != null)
            OnUp.Invoke();
    }
}
