using Godot;
using System;

public class ExitButton : Button
{
        public override void _Pressed()
    {
        //Quit the game
        GetTree().Quit();
    }
}
