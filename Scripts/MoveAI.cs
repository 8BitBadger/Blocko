using Godot;
using System;
using System.Linq;
using EventCallback;
public class MoveAI : Node
{
    //The speed the ai will be able to move
    private int speed = 150;
    //The target the enemy is looking at
    Node2D target;
    //The path the enemy will follow
    Vector2[] path;
    //Check if the target is in the range of the enemies sight range
    bool targetInRange = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Do not run the process funtcion loop until told to
        SetProcess(false);
        //Set the target of the enemy
        target = (Node2D)GetNode<KinematicBody2D>("../../Player");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (targetInRange)
        {
            AIGetPath();
        }

        if (path != null)
        {
            float moveDistance = speed * delta;
            MoveAlongPath(moveDistance);
        }

    }
    private void MoveAlongPath(float distance)
    {
                     //If the enemy is close to the target he stops moving
            if(((Node2D)GetParent()).Position.DistanceTo(target.Position) < 35)
            {
                GD.Print("Enemy is close enough to player");
                //We set the path to null and the n break from the loop
                path = null;
                return;
            }

        Vector2 startPoint = ((Node2D)GetParent()).Position;

        foreach (Vector2 point in path)
        {
            float distanceToNextPoint = startPoint.DistanceTo(point);

            if (distance <= distanceToNextPoint && distance >= 0.0f)
            {
                ((Node2D)GetParent()).Position = startPoint.LinearInterpolate(point, distance / distanceToNextPoint);
                break;
            }
            else if (distance < 0.0f)
            {
                SetProcess(false);
                break;
            }
           
            distance -= distanceToNextPoint;
            startPoint = point;
            path = path.Skip(1).ToArray();
        }
    }
    private void AIGetPath()
    //private void GetPath(SendPathEvent spei)
    {
        RequestPathEvent rpei = new RequestPathEvent();
        rpei.requester = (Node2D)GetParent();
        rpei.target = target;
        rpei.FireEvent();
        path = rpei.path;
    }
    public void BodyEntered(Node node)
    {
        //Im cheating here as the target name is already set
        if (node.Name == target.Name)
        {
            targetInRange = true;
            SetProcess(true);
        }
    }
    public void BodyExited(Node node)
    {
        //Im cheating here as the target name is already set
        if (node.Name == target.Name)
        {
            targetInRange = false;
            SetProcess(false);
        }
    }
    public override void _ExitTree()
    {

    }
}
