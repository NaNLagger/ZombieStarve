using UnityEngine;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode, CustomEditor(typeof(CanBeCollected))]
public class CanBeCollectedEditor : Editor {

    private Item editItem;

    private void Awake() {
        ItemEntities.Init();
        editItem = ((CanBeCollected)target).drop;
    }

    public override void OnInspectorGUI() {
        string[] values = ItemEntities.items.Values.Select(x => x.Name).ToArray<string>();
        int index = values.ToList().IndexOf(editItem.Name);
        int currentIndex = EditorGUILayout.Popup("Select Item", index, values);

        if (GUI.changed) {
            string nName = values[currentIndex];
            if (nName != editItem.Name) {
                ((CanBeCollected)target).drop = ItemEntities.items[nName];
                editItem = ((CanBeCollected)target).drop;
            }
        }
    }
}
