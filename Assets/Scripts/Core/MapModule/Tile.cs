using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

    static float TILE_WIDTH = 10.2f;
    static float TILE_HEIGHT = 5.1f;

    [SerializeField] private Position position;
    public GameObject Grass;

    public GameObject BeachTL;
    public GameObject BeachTR;
    public GameObject BeachBL;
    public GameObject BeachBR;

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
