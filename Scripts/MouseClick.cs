using Godot;
using System;
using EventCallback;

public class MouseClick : Node2D
{
    public override void _Ready()
    {
        InputCallbackEvent.RegisterListener(GetInput);
    }
    private void GetInput(InputCallbackEvent icei)
    {
        if (icei.lmbClickPressed)
        {
            //Call the mouse click event for the player 
            MouseClickEvent mcei = new MouseClickEvent();
            mcei.clickPos = GetGlobalMousePosition();
            mcei.FireEvent();
        }
    }
}
