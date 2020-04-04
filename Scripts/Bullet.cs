using Godot;
using System;
using EventCallback;

public class Bullet : Area2D
{
    [Export]
    int speed = 300;
    Vector2 mousePos;
    Vector2 dir;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Get the direction of the mouse hte moment the bullet is spawned
        mousePos = GetGlobalMousePosition();
        dir = mousePos - GlobalPosition;
        dir = dir.Normalized();
        //Set the bullets position as it does not inherit it any more
        Position = ((Node2D)GetParent().GetParent()).Position;
        //Remove the transform inheritance from the bullets parent
        SetAsToplevel(true);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        //Set the position to the new calculated vector every physics frame
        Position += (speed * delta) * dir;
    }
    public void BodyEntered(Node node)
    {
        GD.Print("Node man = " + node.Name);
        if (node.Name == "TileMap")
        {
            QueueFree();
        }

    }
}
