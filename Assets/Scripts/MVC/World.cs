using UnityEngine;

public class World {

    public Tile[,] tiles;
    public int width {get;}
    public int height{get;}

    public World(int width = 500, int height = 500) {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World created with " + (width * height) + " tiles. ");
    }

    public void RandomizeTiles() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (Random.Range(0, 2) == 0) {
                    tiles[x, y].Type = Tile.TileType.Stone;
                } else {
                    tiles[x, y].Type = Tile.TileType.Dirt;
                }
            }
        }
    }
}
