using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilemapGeneration))]
public class TilemapGenerationEditor : Editor {
    
    public override void OnInspectorGUI() {

        TilemapGeneration tilemapGeneration = (TilemapGeneration) target;
        if (GUILayout.Button("Generate")) {
            tilemapGeneration.GenerateFromTexture();
        }
        if (GUILayout.Button("Clear all tiles")) {
            tilemapGeneration.tilemap.ClearAllTiles();
        }
        
        base.OnInspectorGUI();
    }
}
