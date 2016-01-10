using UnityEngine;
using System.Collections.Generic;

public class TileController : MonoBehaviour {
    
    private Dictionary<string, Tile> tiles = new Dictionary<string, Tile>();

    void Awake() {
        Tile[] tempList = transform.GetComponentsInChildren<Tile>();// Resources.FindObjectsOfTypeAll<Tile>();
        foreach(Tile tile in tempList) {
            addTile(tile);
        }
    }

    private bool addTile(Tile tile) {
        var key = tile.Pos.Column + " " + tile.Pos.Row;
        if(tiles.ContainsKey(key)) {
            Debug.LogError("This tile already was added: " + key);
            return false;
        } else {
            tiles.Add(key, tile);
        }
        return true;
    }

}
