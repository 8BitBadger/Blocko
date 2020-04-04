using Godot;
using System;
using EventCallback;

public class InputControl : Node2D
{
bool upCheck,downCheck,leftCheck, rightCheck;

    public override void _UnhandledInput(Godot.InputEvent @event)
    {
        if (@event is InputEventMouseButton || @event is InputEventKey)
        {
            InputCallbackEvent icei = new InputCallbackEvent();
            if (@event.IsActionPressed("LeftMouseClick"))
            {
                icei.lmbClickPressed = true;
            }
            if (@event.IsActionReleased("LeftMouseClick"))
            {
                icei.lmbClickRelease = true;
            }
            if (@event.IsActionPressed("RightMouseClick"))
            {
                icei.rmbClickPressed = true;
            }
            if (@event.IsActionReleased("RightMouseClick"))
            {
                icei.rmbClickRelease = true;
            }
            if (@event.IsActionPressed("MoveUp"))
            {
                icei.upPressed = true;
            }
            if (@event.IsActionReleased("MoveUp"))
            {
                icei.upRelease = true;
            }
            if (@event.IsActionPressed("MoveDown"))
            {
                icei.downPressed = true;
            }
            if (@event.IsActionReleased("MoveDown"))
            {
                icei.downRelease = true;
            }
            if (@event.IsActionPressed("MoveLeft"))
            {
                icei.leftPressed = true;
            }
            if (@event.IsActionReleased("MoveLeft"))
            {
                icei.leftRelease = true;
            }
            if (@event.IsActionPressed("MoveRight"))
            {
                icei.rightPressed = true;
            }
            if (@event.IsActionReleased("MoveRight"))
            {
                icei.rightRelease = true;
            }

            icei.FireEvent();
        }
    }
}
