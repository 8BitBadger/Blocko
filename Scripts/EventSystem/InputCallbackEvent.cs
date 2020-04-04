using Godot;
using System;

namespace EventCallback
{
    public class InputCallbackEvent : Event<InputCallbackEvent>
    {
        public bool upPressed, downPressed, leftPressed, rightPressed, lmbClickPressed, rmbClickPressed;
        public bool upRelease, downRelease, leftRelease, rightRelease, lmbClickRelease, rmbClickRelease;
    }
}
