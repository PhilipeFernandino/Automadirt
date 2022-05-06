using UnityEngine;

public class WorldController : MonoBehaviour {
    
    public Sprite dirtSprite, stoneSprite;
    public int height, width;

    private World world;
    private float randomizeTileTimer = 2f;
    
    void Start () {
        world = new World(height, width);
        world.RandomizeTiles();

        for (int x = 0; x < world.width; x++) {
            for (int y = 0; y < world.height; y++) {
                GameObject tileGameObject = new GameObject();
                Tile tileData = world.tiles[x, y];
                
                tileGameObject.name = "Tile (" + x + "," + y + ")";
                tileGameObject.transform.position = new Vector3(tileData.x, tileData.y, 0);

                SpriteRenderer sr = tileGameObject.AddComponent<SpriteRenderer>();
                if (tileData.Type == Tile.TileType.Dirt) sr.sprite = dirtSprite;
                if (tileData.Type == Tile.TileType.Stone) sr.sprite = stoneSprite;
                
                tileData.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tileGameObject); } );
            }
        }
    }

    void OnTileTypeChanged(Tile tileData, GameObject tileGameObject) {
        if (tileData.Type == Tile.TileType.Dirt) {
            tileGameObject.GetComponent<SpriteRenderer>().sprite = dirtSprite;
        }
        else tileGameObject.GetComponent<SpriteRenderer>().sprite = stoneSprite;
    }
}
