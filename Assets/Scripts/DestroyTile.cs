using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTile : MonoBehaviour {
    public Tilemap tilemap;
    void Update() {
        if (Input.GetButton("Fire1")) {
            Vector3Int mousePos = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(mousePos);
            mousePos.z = 0;
            if(tilemap.GetTile(mousePos) != null) {
                Debug.Log("Is not null");
                tilemap.SetTile(tilemap.WorldToCell(mousePos), null);
            } 
        }
    }
}