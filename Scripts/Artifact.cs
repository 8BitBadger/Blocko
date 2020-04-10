using Godot;
using System;
using EventCallback;

public class Artifact : Area2D
{
    // Called when the node enters the scene tree for the first time.
    public void BodyEntered(Node node)
    {
        if (node.IsInGroup("Player"))
        {
            WinEvent wei = new WinEvent();
            wei.FireEvent();
        }
    }
}
