using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler {

    public Text Count;

    private Transform startParent;
    private Canvas customParent;
    private Slot slot;
    public ItemStack currentItemStack;

    private void Awake() {
        customParent = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
    }
    
    public void InitItem(ItemStack itemStack) {
        GetComponent<Image>().sprite = itemStack.TypeItem.Icon;
        Count.text = itemStack.Count.ToString();
        currentItemStack = itemStack;
        itemStack.OnChangeCount += ChangeCount;
        itemStack.OnNullCount += Delete;
    }

    public void SetSlot(Slot currentSlot) {
        if (!currentSlot.BlockSlot(this)) {
            transform.SetParent(startParent.transform, false);
            transform.localPosition = new Vector3(0, 0, 0);
            return;
        }
        if(slot != null)
            slot.FreeSlot();
        slot = currentSlot;
        transform.SetParent(currentSlot.transform, false);
        transform.localPosition = Vector2.zero;
    }

    public void Delete() {
        if (slot != null)
            slot.FreeSlot();
        Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(customParent.transform, false);
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
        if (transform.parent == customParent.transform) {
            transform.SetParent(startParent.transform, false);
        }
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ChangeCount() {
        Count.text = currentItemStack.Count.ToString();
    }
}


