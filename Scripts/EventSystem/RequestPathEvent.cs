using Godot;
using System;

namespace EventCallback
{
    public class RequestPathEvent : Event<RequestPathEvent>
    {
        //The target requested by the enemy
        public Node2D target;

        public Node2D requester;

        public Vector2[] path;
    }
}