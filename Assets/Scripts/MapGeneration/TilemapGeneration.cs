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

    private BoundsInt area;
    
    //TODO: Esse método aqui não vai rolar
    //melhor pensar em um que gera os chunks ao redor do player
    public void GenerateFromTexture() {
        
        int ctl = colorToTiles.Count;
        Texture2D texture = noiseVisualizer.GetTexture();
        TileBase[] tiles = new TileBase[mapWidth * mapHeight];
        Color[] texturePixels = texture.GetPixels();

        area.size = new Vector3Int(mapWidth, mapHeight, 1);

        for (int w = 0; w < mapWidth; w++) {
            for (int h = 0; h < mapHeight; h++) {
                for (int c = 0; c < ctl; c++) {
                    if (colorToTiles[c].color == texturePixels[w * mapWidth + h]) {
                        tiles[w * mapWidth + h] = colorToTiles[c].tile;
                    }
                }   
            }
        }
        tilemap.SetTilesBlock(area, tiles);
    }
}
