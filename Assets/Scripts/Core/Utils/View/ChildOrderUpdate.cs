using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ChildOrderUpdate : MonoBehaviour {

    private int startOrder;
    private SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startOrder = spriteRenderer.sortingOrder;
    }

    public void UpdateOrder(int order) {
        spriteRenderer.sortingOrder = order + startOrder;
    }
}
