using Godot;
using System;

public class TileVisibiltyCheck : Node2D
{

    Node2D playerPos;
    Node2D test;

    Sprite icon;

    TileMap map;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //map = GetNode<TileMap>("../TileMap");
        //playerPos = (Node2D)GetNode<KinematicBody2D>("../../Player");
        //icon = GetNode<Sprite>("../Icon");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        //if (playerPos != null && map != null)
        //{
        //float x = Mathf.Clamp(playerPos.Position.DistanceTo(icon.Position) / 250, 0.0f, 1.0f);
        //icon.Material.SetShaderParam("distanceToPlayer", x);
        // (icon.Material as ShaderMaterial).SetShaderParam("distanceToPlayer", x);
        // (map.Material as ShaderMaterial).SetShaderParam("distanceToPlayer", x); // .TileSet.TileGetMaterial(0).SetShaderParam("distanceToPlayer", x);

        //}
        //else
        //{
        //playerPos = (Node2D)GetNode<KinematicBody2D>("../../Player");
        // GD.PrintErr("The player or map could not be found");
        //}
    }
}
