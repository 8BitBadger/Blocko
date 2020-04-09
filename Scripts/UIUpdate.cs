using Godot;
using System;
using EventCallback;

public class UIUpdate : Node2D
{
    TextureProgress healthbar;
    TextureProgress ammobar;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        healthbar = GetNode<TextureProgress>("Health");
        ammobar = GetNode<TextureProgress>("Ammo");

        SendHealthEvent.RegisterListener(UpdateHealth);
        SendAmmoEvent.RegisterListener(UpdateAmmo);
    }

    private void UpdateHealth(SendHealthEvent shei)
    {
        healthbar.Value = shei.health;
    }

    private void UpdateAmmo(SendAmmoEvent saei)
    {
        ammobar.Value = saei.ammo;
    }

    public override void _ExitTree()
    {
        SendHealthEvent.UnregisterListener(UpdateHealth);
        SendAmmoEvent.UnregisterListener(UpdateAmmo);
    }
}
