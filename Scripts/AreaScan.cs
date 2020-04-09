using Godot;
using System;
using EventCallback;

public class AreaScan : Node2D
{
    TileMap map;

    Node2D player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Node2D)GetParent();
        //Grab a refference to the map
        MapInfoRequestEvent mirei = new MapInfoRequestEvent();
        mirei.FireEvent();
        map = mirei.tileMap;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        GetSurroundingTiles();

        // 
    }

    private void GetSurroundingTiles()
    {
        Vector2 tilePos = player.Position / 32;
        //GD.Print("Player tile pos = " + tilePos);

            // (map.Material as ShaderMaterial).SetShaderParam("distanceToPlayer", .5f);          
        /*
        for (int y = (int)((Node2D)GetParent()).Position.y - 5; y < ((Node2D)GetParent()).Position.y + 5; y++)
                {
                    for (int x = (int)((Node2D)GetParent()).Position.x - 5; x < ((Node2D)GetParent()).Position.x + 5; x++)
                    {   

                       (map.Material as ShaderMaterial).SetShaderParam("distanceToPlayer", x);
                    }

                }
                */
    }
}
