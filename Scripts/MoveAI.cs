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
    //The lights for the eyes
    Light2D eyes;
    //Check if the enemy can attack
    bool canAttack = true;
    //The timer for the attack timing
    Timer attackTimer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Do not run the process funtcion loop until told to
        SetProcess(false);
        //Set the target of the enemy
        target = (Node2D)GetNode<KinematicBody2D>("../../Player");
        //Get a refference to th"e lights for the eyes
        eyes = GetNode<Light2D>("../Eyes");
        eyes.Enabled = false;
        attackTimer = GetNode<Timer>("../AttackTimer");
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
    public void AttackTimeOut()
    {
        canAttack = true;
    }
    private void MoveAlongPath(float distance)
    {
        //If the enemy is close to the target he stops moving
        if (((Node2D)GetParent()).Position.DistanceTo(target.Position) < 35)
        {
            if (!canAttack) return;
            //We set the path to null and the n break from the loop
            HitEvent hei = new HitEvent();
            hei.attacker = (Node2D)GetParent();
            hei.target = target;
            hei.damage = 5;
            hei.FireEvent();

            canAttack = false;
            attackTimer.Start();

            path = null;
            return;
        }

        //Stop the attack timer becuase the target has moved out of range
        attackTimer.Stop();

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
            eyes.Enabled = true;
            targetInRange = true;
            SetProcess(true);
        }
    }
    public void BodyExited(Node node)
    {
        if(target == null) return;
        //Im cheating here as the target name is already set
        if (node.Name == target.Name)
        {
            eyes.Enabled = false;
            targetInRange = false;
            SetProcess(false);
        }
    }
}

