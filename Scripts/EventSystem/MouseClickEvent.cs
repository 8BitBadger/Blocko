using Godot;
using System;

namespace EventCallback
{
    public class MouseClickEvent : Event<MouseClickEvent>
    {
        public Vector2 clickPos;
    }
}