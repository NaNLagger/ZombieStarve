using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler {

    public Text Count;

    private Vector3 startPostion;
    private Transform startParent;

    private Transform customParent;
    private Slot slot;
    
    public void InitItem(Item item, int count) {
        GetComponent<Image>().sprite = item.Icon;
        Count.text = count.ToString();
    }

    public void SetSlot(Slot currentSlot) {
        if (currentSlot.block) {
            transform.parent = startParent;
            transform.localPosition = new Vector3(0, 0, 0);
            return;
        }
        if (slot != null)
            slot.block = false;
        slot = currentSlot;
        transform.parent = currentSlot.transform;
        transform.localPosition = Vector2.zero;
        currentSlot.block = true;
    }

    private void Awake() {
        customParent = transform.parent.parent.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startPostion = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.parent = customParent;
    }

    public void OnDrag(PointerEventData eventData) {        
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData) {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        if (raycastResults.Count > 0) {
            foreach (RaycastResult target in raycastResults) {
                if (target.gameObject.GetComponent<Slot>() != null) {
                    SetSlot(target.gameObject.GetComponent<Slot>());
                }
            }
        }
        if (transform.parent == customParent) {
            transform.parent = startParent;
        }
        transform.localPosition = new Vector3(0, 0, 0);
    }
}


