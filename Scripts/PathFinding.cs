using Godot;
using System;
using EventCallback;

public class PathFinding : Node
{
    Navigation2D nav2D;
    Vector2 target;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        RequestPathEvent.RegisterListener(GetPath);

        nav2D = GetNode<Navigation2D>("../Nav2D");
        //Get the ti;e map and the naviation map here
    }

    private void GetPath(RequestPathEvent rpei)
    {
        //Get the generated path
        Vector2[] path = nav2D.GetSimplePath(rpei.requester.GlobalPosition, rpei.target.GlobalPosition);
        rpei.path = path;
    }

    public override void _ExitTree()
    {
        RequestPathEvent.UnregisterListener(GetPath);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
