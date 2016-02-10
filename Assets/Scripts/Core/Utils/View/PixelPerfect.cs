using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera)), ExecuteInEditMode]
public class PixelPerfect : MonoBehaviour {

    public float minOrtho = 7;
    public int PPU = 32;
    private int currentResolution;

    void Awake() {
        UpdateOrthoSize();
    }

    void Update() {
#if UNITY_EDITOR
        if (currentResolution != Screen.height)
            UpdateOrthoSize();
#endif
    }

    void UpdateOrthoSize() {
        currentResolution = Screen.height;
        float orthoSize = (currentResolution / (float)PPU) * 0.5f;
        orthoSize = orthoSize >= minOrtho * 2 ? orthoSize % minOrtho : orthoSize;
        GetComponent<Camera>().orthographicSize = orthoSize;
    }
}
