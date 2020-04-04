using Godot;
using System;
using System.Linq;
using EventCallback;

public class Lighting : Sprite
{
    //The light values for each tile
    float[,] lightValues;
    //Refference to the player node
    Node2D player;
    //The size of the tiles the shader covers
    Vector2 sizeInTiles;
    //The size of the shader sprite that covers the tiles
    Vector2 size;
    //The size of the map
    Vector2 mapSize;
    //The step size for the shader image that covers the tile maps
    int snapStep = 128;
    //The refference to the tile map for late use in the class
    TileMap tileMap;
    //The map info requies info that will be used multiple times during this classes function 
    MapInfoRequestEvent mirei = new MapInfoRequestEvent();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Grab the map size from the map
        mirei.FireEvent();
        mapSize = mirei.mapSize;
        //Grab the player node 
        player = (Node2D)GetNode<KinematicBody2D>("../Player");
        //Set the lightValues 2d arraay size to the size of the map
        lightValues = new float[(int)mapSize.x, (int)mapSize.y];
    }

    private void SetSize()
    {
        //Get the size of the wievport and add the snap step to it to completely cover the screen
        size = GetViewportRect().Size + new Vector2(snapStep, snapStep) * 2;
        //Grab the zoom level of the camera 
        size *= player.GetNode<Camera2D>("Camera2D").Zoom;
        //Make it so the size is always divisible by 32
        size.x = Mathf.Stepify(size.x, 32);
        size.y = Mathf.Stepify(size.y, 32);
        //Set the size in tiles to the size the shader is covering
        sizeInTiles.x = (int)size.x / 16;
        sizeInTiles.y = (int)size.y / 16;
        //Set the shader scaled size to the size we got from the camera and snap steps
        Scale = size;
    }

    private void InitBlockLights()
    {
        //Grab an updated refference to the tile map
        mirei.FireEvent();
        tileMap = mirei.tileMap;
        //Iterate throught the 2d array values
        for (int x = 0; x < lightValues.GetLength(0); x++)
        {
            for (int y = 0; y < lightValues.GetLength(1); y++)
            {
                //If the tile is a floor then it is fulle lit
                if (tileMap.GetCell(x, y) == 0)
                {
                    lightValues[x, y] = 1;
                }
                else
                {
                    lightValues[x, y] = 0;
                }
            }
        }
    }

    private void SetPosition()
    {
        float posX = Mathf.Stepify(player.Position.x, snapStep) - size.x / 2;
        float posY = Mathf.Stepify(player.Position.y, snapStep) - size.y / 2;

        Position = new Vector2(posX, posY);
    }
    private bool WithinBounds(int x, int y)
    {
        //Check if the block passed to it is in the bounds of the light values index sizes
        return (x < lightValues.GetLength(0) && x >= 0 && y < lightValues.GetLength(1) && y >= 0);
    }

    private void SetShaderTiles()
    {
        int startX = (int)Position.x / 16;
        int startY = (int)Position.y / 16;

        float[] regionTileInfo;

        for (int y = startY; y < startY + sizeInTiles.y; y++)
        {
            for (int x = startX; x < startX + sizeInTiles.x; x++)
            {
regionTileInfo[lightValues[x, y]]
            }
        }


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        SetPosition();
    }
}
