using Godot;
using System;
using EventCallback;

public class Health : Node
{
    //Make the health of the enemy tank set able outside in the inspector
    [Export]
    int health = 100;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        HitEvent.RegisterListener(OnHit);
    }
    private void OnHit(HitEvent hei)
    {
        if (GetParent().Name == hei.target.Name)
        {
            //Reduce the tanks health with the given amount
            health -= hei.damage;
            //Broadcast the health after it has been modified to anyone who is listening
            SendHealthEvent shei = new SendHealthEvent();
            shei.health = health;
            shei.target = hei.target;
            shei.FireEvent();
            //Check if the health has gone down to zero
            CheckHealth();
        }
    }
    private void CheckHealth()
    {
        if (health <= 0)
        {
            //Make sure the health does not go below 0
            health = 0;
            Die();
        }
    }
    void Die()
    {
        // I am dying for some reason.
        DeathEvent dei = new DeathEvent();
        dei.Description = "Unit " + this.Name + " has died.";
        dei.target = (Node2D)GetParent();
        dei.FireEvent();

        //Remove the parent node forn the scene
        dei.target.QueueFree();
    }
    public override void _ExitTree()
    {
        HitEvent.UnregisterListener(OnHit);
    }
}

