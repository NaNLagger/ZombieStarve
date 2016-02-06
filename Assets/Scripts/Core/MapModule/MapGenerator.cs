using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

    [SerializeField] private int width;
    [SerializeField] private int height;

    [Tooltip("GrassBiome")]
    public Tile GrassTile;
    public Sprite[] GrassSprites;

    [Tooltip("WaterBiome")]
    public Tile WaterTile;
    public Sprite[] WaterSprites;

    private Dictionary<Position, int> positions = new Dictionary<Position, int>();

    private void Awake() {
        Generate();
    }

    private void Generate() {
        ClearMap();
        GenerateHeight();
        foreach(KeyValuePair<Position, int> pair in positions) {
            Tile tile;
            if (pair.Value == 1) {
                tile = Instantiate(GrassTile).GetComponent<Tile>();
                tile.Grass.GetComponent<SpriteRenderer>().sprite = GrassSprites[Random.Range(0, GrassSprites.Length)];
                
            } else {
                tile = Instantiate(WaterTile).GetComponent<Tile>();
                tile.GetComponent<SpriteRenderer>().sprite = WaterSprites[Random.Range(0, WaterSprites.Length)];
            }

            tile.transform.parent = transform;
            tile.Pos = pair.Key;
            if(pair.Value == 1) {
                AddingBeach(tile);
            }
        }
    }

    private void GenerateHeight() {
        int count = (width + height) / 10 + 1;
        int w, h;
        for(int i = 0; i < count; i++) {
            w = Random.Range(-width / 2, width / 2);
            h = Random.Range(-height / 2, height / 2);
            FillLand(new Position(w, h));
        }
    }

    private void FillLand(Position pos) {
        int countCol = Random.Range(2, 10);
        for(int i = -countCol/2; i < countCol/2; i++) {
            int countRow = Random.Range(2, 10);
            for(int j = -countRow; j < countRow/2; j++) {
                Position temp = new Position(i, j) + pos;
                if (positions.ContainsKey(temp)) {
                    positions[temp] = 1;
                }
            }
        }
    }

    private void ClearMap() {
        for (int i = -width / 2; i < width / 2; i++) {
            for (int j = -height / 2; j < height / 2; j++) {
                positions.Add(new Position(i, j), 0);
            }
        }
    }

    private void AddingBeach(Tile tile) {
        if (positions.ContainsKey(tile.Pos.NeighborTR()) && positions[tile.Pos.NeighborTR()] == 0) {
            tile.BeachTR.SetActive(true);
        }
        if (positions.ContainsKey(tile.Pos.NeighborTL()) && positions[tile.Pos.NeighborTL()] == 0) {
            tile.BeachTL.SetActive(true);
        }
        if (positions.ContainsKey(tile.Pos.NeighborBR()) && positions[tile.Pos.NeighborBR()] == 0) {
            tile.BeachBR.SetActive(true);
        }
        if (positions.ContainsKey(tile.Pos.NeighborBL()) && positions[tile.Pos.NeighborBL()] == 0) {
            tile.BeachBL.SetActive(true);
        }
    }

    private void AddingRoad(Tile tile) {

    }
}
