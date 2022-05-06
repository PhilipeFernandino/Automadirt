using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemaptest : MonoBehaviour {
    public int height, width;
    public Tilemap tilemap;
    public TileBase dirt, stone;
    public Transform playerTransform;
    public int generateDistance;

    private TileType[,] tileArray;
    private enum TileType { Empty, Dirt, Stone };
    private Vector3 prevPlayerPosition;

    private void Start() {
        tileArray = new TileType[width, height];
        playerTransform.position = new Vector3(5, height + 5, 0);
        prevPlayerPosition = playerTransform.position;
    }

    private void Update() {
        GenerateFromTextureAroundPlayer();
    }

    private void GenerateFromTextureAroundPlayer() {

        Vector3 playerPos = playerTransform.position;

        if (prevPlayerPosition == playerPos) return; 
        prevPlayerPosition = playerPos;

        for (int x = -generateDistance; x < generateDistance; x++) {
            for (int y = -generateDistance; y < generateDistance; y++) {
        
                Vector3Int pos = new Vector3Int((int) playerPos.x + x, (int) playerPos.y + y, 0);
        
                if (Helper.IsVec2IntInBoundaries((Vector2Int) pos, 0, 0, width, height) && tileArray[pos.x, pos.y] == TileType.Empty) {
                    int r = Random.Range(0, 2);
                    tileArray[pos.x, pos.y] = (r == 0) ? TileType.Dirt : TileType.Stone;

                    TileBase tile = (r == 0) ? dirt : stone;
                    tilemap.SetTile(pos, tile); 
                }

            }
        }
    }   
}
