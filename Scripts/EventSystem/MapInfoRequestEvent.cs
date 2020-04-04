using Godot;
using System;

namespace EventCallback
{
    public class MapInfoRequestEvent : Event<MapInfoRequestEvent>
    {
        public Vector2 mapSize;

        public TileMap tileMap;

    }
}
