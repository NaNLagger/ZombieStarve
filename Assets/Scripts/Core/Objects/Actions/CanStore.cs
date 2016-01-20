using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CanDrop)), RequireComponent(typeof(SimpleObject))]
public class CanStore : MonoBehaviour {

    public int Count = 1;
    public List<ItemStack> storedItems = new List<ItemStack>();

    public delegate void ItemEvent(ItemStack stack);
    public ItemEvent OnInsertStack;

    private void Awake() {
        GetComponent<SimpleObject>().OnDeath += Drop;
    }

    public bool Store(Item item) {
        return Store(new ItemStack(item));
    }

    public bool Store(ItemStack stack) {
        var findResult = storedItems.Find(x => x.TypeItem.Equals(stack.TypeItem) && !x.isFull());
        if(findResult != null) {
            var zuViel = findResult.AddStack(stack);
            if (zuViel != null && zuViel.Count != 0) {
                Store(zuViel);
            }
            return true;
        }
        else {
            return AddNewStack(stack);
        }
    }

    public bool AddNewStack(ItemStack stack) {
        if (storedItems.Contains(stack))
            return false;
        if (storedItems.Count < Count) {
            storedItems.Add(stack);
            if (OnInsertStack != null)
                OnInsertStack(stack);
            return true;
        }
        return false;
    }

    public void DropStack(ItemStack stack) {
        var canDropComponent = GetComponent<CanDrop>();
        canDropComponent.dropStacks.Add(stack);
        storedItems.Remove(stack);
        /*GOVNO CODE BEGIN*/
        if(stack.OnNullCount != null)
            stack.OnNullCount();
        /*GOVNO CODE END*/
        canDropComponent.Drop();
    }

    public void DeleteStack(ItemStack stack) {
        storedItems.Remove(stack);
    }

    public void Split(ItemStack stack) {
        var nStack = stack.Split();
        if (nStack != null) {
            if (!AddNewStack(nStack))
                DropStack(nStack);
        }
    }

    public void Drop() {
        var canDropComponent = GetComponent<CanDrop>();
        foreach(ItemStack stack in storedItems) {
            canDropComponent.dropStacks.Add(stack);
            /*GOVNO CODE BEGIN*/
            if (stack.OnNullCount != null)
                stack.OnNullCount();
            /*GOVNO CODE END*/
        }
        storedItems.Clear();
        canDropComponent.Drop();
    }

}
