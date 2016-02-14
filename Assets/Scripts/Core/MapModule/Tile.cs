using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

    static float TILE_WIDTH = 8f;
    static float TILE_HEIGHT = 4f;

    [SerializeField] private Position position;

    public Position Pos {
        get {
            return position;
        }
        set {
            position = value;
            ChangePos();
        }
    }

    private void Awake() {
        ChangePos();
    }

    private void ChangePos() {
        transform.position = Pos.ToPoint(TILE_WIDTH, TILE_HEIGHT);
    }
}
