
using Godot;
using System;
using EventCallback;

public class Weapon : Node2D
{
    PackedScene bulletScene = new PackedScene();
    //The bullet node when the ullterom
    Node bullet;
    //The amount of brick the player has broken and saved as ammo
    int ammo;
    public override void _Ready()
    {
        TileDestroyEvent.RegisterListener(AddAmmo);

        bulletScene = ResourceLoader.Load("res://Scenes/Bullet.tscn") as PackedScene;
    }
    private void AddAmmo(TileDestroyEvent tdei)
    {
        //Check if the tile destroyed bool is true
        if (tdei.tileDestroyed)
        {
            //Add ammo if a tile was destroyed
            ammo++;
        }
        else
        {
            if (ammo > 0)
            {
                //Create a ne bullet
                bullet = bulletScene.Instance();
                //Set the direction of the bullet when instanced
                AddChild(bullet);
                
                ammo--;
            }
            //Chekc the info frkm
            //Shoot the darn brick here lol
        }
    }
    public override void _ExitTree()
    {
        TileDestroyEvent.UnregisterListener(AddAmmo);
    }
}
