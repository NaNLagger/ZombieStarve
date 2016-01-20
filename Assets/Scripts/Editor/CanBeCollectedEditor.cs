using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

[ExecuteInEditMode, CustomEditor(typeof(CanBeCollected))]
public class CanBeCollectedEditor : Editor {

    private void Awake() {
        ItemEntities.Init();
    }

    public override void OnInspectorGUI() {
        var editItem = ((CanBeCollected)target).drop;

        string[] values = ItemEntities.items.Values.Select(x => x.Name).ToArray<string>();

        int index = values.ToList().IndexOf(editItem.TypeItem.Name);
        int currentIndex = EditorGUILayout.Popup("Select Item", index, values);

        editItem.Count = EditorGUILayout.IntSlider("Count", editItem.Count, 1, 20);

        if (GUI.changed) {
            string nName = values[currentIndex];
            if (nName != editItem.TypeItem.Name) {
                editItem.TypeItem = ItemEntities.items[nName];
            }
        }
    }
}
