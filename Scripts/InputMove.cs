using Godot;
using System;
using EventCallback;

public class InputMove : KinematicBody2D
{
    [Export]
    int speed = 350;
    //The movement keys are pressed or not
    bool up, down, left, right;
    //the velocity of the charecter
    Vector2 velocity = new Vector2();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        InputCallbackEvent.RegisterListener(GetInput);
    }
    private void GetInput(InputCallbackEvent icei)
    {
        if (icei.upPressed) up = true;
        else if (icei.upRelease) up = false;

        if (icei.downPressed) down = true;
        else if (icei.downRelease) down = false;

        if (icei.leftPressed) left = true;
        else if (icei.leftRelease) left = false;

        if (icei.rightPressed) right = true;
        else if (icei.rightRelease) right = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        if (up) velocity.y = -1;
        else if (down) velocity.y = 1;
        else velocity.y = 0;
        if (left) velocity.x = -1;
        else if (right) velocity.x = 1;
        else velocity.x = 0;

        velocity = velocity.Normalized() * speed;

        MoveAndSlide(velocity);
    }
    public override void _ExitTree()
    {
        InputCallbackEvent.UnregisterListener(GetInput);
    }
}
