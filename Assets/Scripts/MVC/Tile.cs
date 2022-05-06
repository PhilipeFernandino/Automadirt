using System;

public class Tile {
    
    Action<Tile> TileTypeChanged;
    
    public enum TileType { Empty, Dirt, Stone, Gold };
    
    public int x {get;}
    public int y {get;}
    
    private TileType type = TileType.Empty;
    public TileType Type {
        get => type;
        set {
            TileType oldType = type;
            type = value;
            
            if (oldType != type && TileTypeChanged != null) TileTypeChanged(this);
        }
    }

    private World world;
    
    public Tile(World world, int x, int y) {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback) {
        TileTypeChanged += callback;
    }
}
