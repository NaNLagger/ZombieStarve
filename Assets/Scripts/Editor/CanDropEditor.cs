using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections;

[ExecuteInEditMode, CustomEditor(typeof(CanDrop))]
public class CanDropEditor : Editor {

    private Item editItem;
    private int size;

    private void Awake() {
        ItemEntities.Init();
        size = ((CanDrop)target).dropItems.Count;
    }

    public override void OnInspectorGUI() {
        DrawSize();
        var items = ((CanDrop)target).dropItems;
        for(int i = 0; i < items.Count; i++) {
            DrawElement(i);
        }
    }

    private void DrawSize() {
        var nSize = EditorGUILayout.IntField(((CanDrop)target).dropItems.Count);

        if(GUI.changed) {
            if(nSize != ((CanDrop)target).dropItems.Count) {
                ChangeSize(nSize);
            }
        }
    }

    private void DrawElement(int position) {
        editItem = ((CanDrop)target).dropItems[position];
        string[] values = ItemEntities.items.Values.Select(x => x.Name).ToArray<string>();
        int index = values.ToList().IndexOf(editItem.Name);
        int currentIndex = EditorGUILayout.Popup("Select Item " + position, index, values);

        if (GUI.changed) {
            string nName = values[currentIndex];
            if (nName != editItem.Name) {
                ((CanDrop)target).dropItems[position] = ItemEntities.items[nName];
                editItem = ((CanDrop)target).dropItems[position];
            }
        }
    }

    private Item GetItem(int position) {
        return ((CanDrop)target).dropItems[position];
    } 

    private int GetSize() {
        return ((CanDrop)target).dropItems.Count;
    }

    private void ChangeSize(int nSize) {
        var items = ((CanDrop)target).dropItems;
        if (nSize > items.Count) {
            for(int i = items.Count; i < nSize; i++) {
                items.Add(ItemEntities.items.Values.First());
            }
        } else {
            for (int i = items.Count - 1; i >= nSize; i--) {
                items.RemoveAt(i);
            }
        }
    }
}
