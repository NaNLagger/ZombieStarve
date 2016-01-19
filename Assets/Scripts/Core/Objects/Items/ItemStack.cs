using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class ItemStack {

    private Item item;
    private int count;

    private const int maxCount = 20;

    public Utils.EmptyEvent OnChangeCount;
    public Utils.EmptyEvent OnNullCount;

    public Item TypeItem {
        get {
            return item;
        }
    }

    public int Count {
        get {
            return count;
        }
        set {
            count = value;
            if (count <= 0) {
                count = 0;
                if (OnNullCount != null)
                    OnNullCount();
            }
            if(count > maxCount) {
                count = maxCount;
            }
            if (OnChangeCount != null)
                OnChangeCount();
        }
    }

    public ItemStack(Item item, int size) {
        this.item = item;
        this.count = size;
    }

    public ItemStack(Item item): this(item, 1) {
    }

    public ItemStack Split() {
        var nCount = count / 2;
        Count = Count - nCount;
        if (nCount == 0)
            return null;
        return new ItemStack(item, nCount);
    }

    public ItemStack AddStack(ItemStack stack) {
        if (!TypeItem.Equals(stack.TypeItem))
            return stack;
            
        var nCount = count + stack.Count;
        if (nCount > maxCount) {
            Count = maxCount;
            stack.Count = nCount - maxCount;
        }
        else {
            Count = nCount;
            stack.Count = 0;
        }
        return stack;
    }

    public bool isFull() {
        return maxCount == count;
    }
}
