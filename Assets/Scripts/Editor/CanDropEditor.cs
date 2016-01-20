using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections;

[ExecuteInEditMode, CustomEditor(typeof(CanDrop))]
public class CanDropEditor : Editor {

    private ItemStack editItem;

    private void Awake() {
        ItemEntities.Init();
    }

    public override void OnInspectorGUI() {
        DrawSize();
        var items = ((CanDrop)target).dropStacks;
        for(int i = 0; i < items.Count; i++) {
            DrawElement(i);
        }
    }

    private void DrawSize() {
        var nSize = EditorGUILayout.IntField(((CanDrop)target).dropStacks.Count);

        if(GUI.changed) {
            if(nSize != ((CanDrop)target).dropStacks.Count) {
                ChangeSize(nSize);
            }
        }
    }

    private void DrawElement(int position) {
        editItem = ((CanDrop)target).dropStacks[position];
        string[] values = ItemEntities.items.Values.Select(x => x.Name).ToArray<string>();
        int index = values.ToList().IndexOf(editItem.TypeItem.Name);
        int currentIndex = EditorGUILayout.Popup("Select Item " + position, index, values);

        if (GUI.changed) {
            string nName = values[currentIndex];
            if (nName != editItem.TypeItem.Name) {
                ((CanDrop)target).dropStacks[position].TypeItem = ItemEntities.items[nName];
                editItem = ((CanDrop)target).dropStacks[position];
            }
        }
    }

    private ItemStack GetItem(int position) {
        return ((CanDrop)target).dropStacks[position];
    } 

    private int GetSize() {
        return ((CanDrop)target).dropStacks.Count;
    }

    private void ChangeSize(int nSize) {
        var items = ((CanDrop)target).dropStacks;
        if (nSize > items.Count) {
            for(int i = items.Count; i < nSize; i++) {
                items.Add(new ItemStack(ItemEntities.items.Values.First()));
            }
        } else {
            for (int i = items.Count - 1; i >= nSize; i--) {
                items.RemoveAt(i);
            }
        }
    }
}
