using Godot;
using System;

namespace EventCallback
{
    public class TileDestroyEvent : Event<TileDestroyEvent>
    {
        public bool tileDestroyed = false;
    }
}