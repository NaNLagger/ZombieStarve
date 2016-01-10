using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

[ExecuteInEditMode, CustomEditor(typeof(ItemCreator))]
public class ItemEditor : Editor {

    private Item addItem;
    private Item editItem;
    
    void Awake() {
        addItem = ((ItemCreator)target).addItem;
        editItem = ((ItemCreator)target).editItem;
    }

    public override void OnInspectorGUI() {
        if (GUILayout.Button("Update Item Entities")) {
            ItemEntities.Update();
        }
        AddItemGUI();
        EditItemGUI();
    }

    private void AddItemGUI() {

        EditorGUI.indentLevel = 1;

        EditorGUILayout.LabelField("Add Item", EditorStyles.boldLabel);

        EditorGUI.indentLevel = 2;

        var name = EditorGUILayout.TextField("Name:", addItem.name);
        var icon = EditorGUILayout.TextField("Icon:", addItem.icon);
        var sprite = EditorGUILayout.TextField("Sprite:", addItem.sprite);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        if (GUILayout.Button("Add Item")) {
            ResourcesLoader.SaveObject("Assets/Resources/Entities/Items/", addItem.name, addItem);
            ItemEntities.Update();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel = 1;

        if (GUI.changed) {
            addItem.name = name;
            addItem.icon = icon;
            addItem.sprite = sprite;
        }
    }

    private void EditItemGUI() {
        EditorGUI.indentLevel = 1;
        EditorGUILayout.LabelField("Edit Item", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 2;
        string[] values = ItemEntities.items.Values.Select(x => x.Name).ToArray<string>();
        int index = values.ToList().IndexOf(editItem.Name);
        int currentIndex = EditorGUILayout.Popup("Select Item", index, values);
        var icon = EditorGUILayout.TextField("Icon:", editItem.icon);
        var sprite = EditorGUILayout.TextField("Sprite:", editItem.sprite);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        if (GUILayout.Button("Edit Item")) {
            ResourcesLoader.SaveObject("Assets/Resources/Entities/Items/", editItem.name, editItem);
            ItemEntities.Update();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel = 1;

        if (GUI.changed) {
            string nName = values[currentIndex];
            if (nName != editItem.Name) {
                editItem = ItemEntities.items[nName];
            }
            else {
                editItem.icon = icon;
                editItem.sprite = sprite;
            }
        }
    }

    private void SaveItem(string name, object item) {
        string path = "Assets/Resources/Entities/Items/";
        string cName = "Item";

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path + name + "." + cName,
                                 FileMode.Create,
                                 FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, item);
        stream.Close();
    }

    private Item LoadItem(string name) {
        string path = "Assets/Resources/Entities/Items/";
        string cName = "Item";

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path + name + "." + cName,
                                  FileMode.Open,
                                  FileAccess.Read,
                                  FileShare.Read);
        Item obj = (Item) formatter.Deserialize(stream);
        stream.Close();
        return obj;
    }

}
