using Godot;
using System;
using EventCallback;

public class Map : Node2D
{
    [Export]
    //The tile size of the map
    Vector2 mapSize = new Vector2(200, 100);
    //The tile data for the tile map
    Tile[,] tileDataMap;
    //The refeference for the map scenes TileMap
    TileMap tileMap;
    //The noise patern for hte map generation
    OpenSimplexNoise noise;
    RandomNumberGenerator random = new RandomNumberGenerator();
    //Grab mouse clikced signal from the input manager, might need to assign input manager tot a node 2D in the main scnene itself
    public override void _Ready()
    {
        //random.Seed = 21;
        random.Randomize();
        //Create ne reference for the Simplex noise to be used for map generation
        noise = new OpenSimplexNoise();
        //Generate a random noise patern by using a randomized seed
        noise.Seed = (int)random.Randi();
        noise.Octaves = 1;
        noise.Period = 12;
        //noise.Persistence = 0.7f;

        MouseClickEvent.RegisterListener(CheckTileClicked);
        MapInfoRequestEvent.RegisterListener(GetMapInfo);

        //Set the tile map data size
        tileDataMap = new Tile[(int)mapSize.x, (int)mapSize.y];
        //Grab a refference to the tile map node
        tileMap = GetNode<TileMap>("Nav2D/TileMap");
        //Initialize the tile data for every entry in the array
        InitMap();
        //Init test of the tile map generation
        GenerateMap();
    }
    private void InitMap()
    {
        //Populate the map with new tiles
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                tileDataMap[x, y] = new Tile();
            }
        }
    }
    private void GenerateMap()
    {
        //Set all the tiles to floor tiles and create a wall around the whole map area
        CreateBlankMap();
        //WallMap();
        CreateTileMap();
    }
    private void CreateBlankMap()
    {
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                float a = noise.GetNoise2d(x, y);
                if (a < 0.0005f)
                {
                    tileDataMap[x, y].type = TileType.WALL_BREAKABLE;
                }
                else if (a < 0.7f)
                {
                    tileDataMap[x, y].type = TileType.FLOOR;
                }
                else
                {
                    tileDataMap[x, y].type = TileType.TREASURE;
                }
                //Set the for edges of the map to be walls
                if (x == 0) tileDataMap[x, y].type = TileType.WALL;
                if (y == 0) tileDataMap[x, y].type = TileType.WALL;
                if (x == mapSize.x - 1) tileDataMap[x, y].type = TileType.WALL;
                if (y == mapSize.y - 1) tileDataMap[x, y].type = TileType.WALL;
                //Set the content of the map to be floors
            }
        }
    }
    private void CreateTileMap()
    {
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                tileMap.SetCellv(new Vector2(x, y), (int)tileDataMap[x, y].type, false, false, false);
            }
        }
        tileMap.UpdateBitmaskRegion(new Vector2(0, 0), mapSize);

    }
    private void CheckTileClicked(MouseClickEvent mcei)
    {
        //Devide the mouse click loction with the tile size to get the correct click position on your map, assuming the tilemap is set to 0,0 position
        Vector2 mapPos = mcei.clickPos / 32;
        //Grab the players position to determin if the player is close enough to break the tile
        Vector2 playerPos = GetNode<KinematicBody2D>("../Player").GlobalPosition;
        //Fire the tile destroy event to add ammo to the player
        TileDestroyEvent tdei = new TileDestroyEvent();
        if (tileMap.GetCellv(mapPos) == (int)TileType.WALL_BREAKABLE && mcei.clickPos.DistanceTo(playerPos) < 150)
        {
            //Replace the tile instabce with the floor
            tileMap.SetCellv(mapPos, (int)TileType.FLOOR);
            //Set the tile destroyed event to true
            tdei.tileDestroyed = true;
            //Update the map once a tile has been changed
            tileMap.UpdateBitmaskRegion(mapPos - Vector2.One, mapPos + Vector2.One);
        }
        tdei.FireEvent();
    }

    private void GetMapInfo(MapInfoRequestEvent mirei)
    {
        mirei.mapSize = mapSize;
        mirei.tileMap = tileMap;
    }
    public override void _ExitTree()
    {
        MouseClickEvent.UnregisterListener(CheckTileClicked);
        MapInfoRequestEvent.UnregisterListener(GetMapInfo);
    }
}