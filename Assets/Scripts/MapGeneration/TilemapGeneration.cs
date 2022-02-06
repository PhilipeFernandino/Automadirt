using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ColorToTile {
    public Color color;
    public TileBase tile;
}

public class TilemapGeneration : MonoBehaviour {

    public NoiseVisualizer noiseVisualizer; 
    public Tilemap tilemap;
    public List<ColorToTile> colorToTiles;
    
    public int mapHeight, mapWidth;

    public void GenerateFromTexture() {
        
        Debug.Log("Gerando");
        int ctl = colorToTiles.Count;
        BoundsInt area = new BoundsInt();
        area.size = new Vector3Int(500, 500, 0);
        Texture2D texture = noiseVisualizer.GetTexture();
        TileBase[] tiles = new TileBase[mapWidth * mapHeight];
        
    
        for (int w = 0; w < mapWidth; w++) {
            for (int h = 0; h < mapHeight; h++) {
                for (int c = 0; c < ctl; c++) {
                    if (colorToTiles[c].color == texture.GetPixel(w, h)) {
                        Debug.Log("Setando tile");
                        tilemap.SetTile(new Vector3Int(w, h, 1), colorToTiles[c].tile);
                    }
                        // tiles[w * mapWidth + h] = colorToTiles[c].tile;
                }   
            }
        }
        // tilemap.SetTilesBlock(area, tiles);
    }
}
