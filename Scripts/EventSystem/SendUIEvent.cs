using Godot;
using System;
namespace EventCallback
{

    public class SendUIEvent : Event<SendUIEvent>
    {
        public bool showMenu, showUI, showWinScreen, showDeathScreen, startGame;
    }

}
