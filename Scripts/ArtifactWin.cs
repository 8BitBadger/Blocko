using Godot;
using System;

public class ArtifactWin : Sprite
{
    float spinSpeed = .5f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Rotation > 360) Rotation = 0;
        Rotation += spinSpeed * delta;
    }
}
