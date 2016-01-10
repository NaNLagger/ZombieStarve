using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;

public static class ResourcesLoader {
    public static Sprite LoadSprite(string name) {
        string folderSprite = "Sprites/";
        return Resources.Load(folderSprite + name, typeof(Sprite)) as Sprite;
    }

    public static GameObject LoadPref(string name) {
        string folderPref = "Prefabs/";
        return Resources.Load(folderPref + name, typeof(GameObject)) as GameObject;
    }

    public static void SaveObject(string path, string name, object item) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path + name + ".txt",
                                 FileMode.Create,
                                 FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, item);
        stream.Close();
    }

    public static object LoadObject(string path, string name) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path + name,
                                  FileMode.Open,
                                  FileAccess.Read,
                                  FileShare.Read);
        object obj = formatter.Deserialize(stream);
        stream.Close();
        return obj;
    }

    public static object LoadObject(TextAsset file) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream(file.bytes);
        object obj = formatter.Deserialize(stream);
        stream.Close();
        return obj;
    }

    public static object[] LoadAllObjectInDirectory(string path) {
        Object[] texts = Resources.LoadAll(path);
        List<object> result = new List<object>();
        foreach (TextAsset file in texts) {
            try {
                result.Add(LoadObject(file));
            } catch (SerializationException) {
                Debug.Log("Not object");
            }
        }
        return result.ToArray();
    }
}