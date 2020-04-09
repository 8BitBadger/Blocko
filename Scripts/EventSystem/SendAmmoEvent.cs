using Godot;
using System;

namespace EventCallback
{
    public class SendAmmoEvent : Event<SendAmmoEvent>
    {
        //The amount of ammo left
       public int ammo;
    }
}
