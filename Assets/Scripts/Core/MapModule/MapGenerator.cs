using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    [SerializeField] private int width;
    [SerializeField] private int height;
    
    private void Awake() {
        Generate();
    }

    private void Generate() {
        for(int i = -width/2; i < width/2; i++) {
            for(int j = -height/2; j < height/2; j++) {
                var type = Random.Range(0,20);
                Tile tile = Instantiate(ResourcesLoader.LoadPref(type == 0 ? "TileWater" : "TileGrass")).GetComponent<Tile>();
                tile.transform.parent = transform;
                tile.Pos = new Position(i, j);
            }
        }
    }
}
