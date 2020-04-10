using Godot;
using System;
using EventCallback;

public class UIControl : CanvasLayer
{
    Node2D ui;
    Node2D menu;
    Node2D winScreen;
    Node2D DeathScreen;
    public override void _Ready()
    {
        WinEvent.RegisterListener(ShowWinScreen);
        DeathEvent.RegisterListener(ShowDeathScreen);
        //Grab a refference to all the ui screens for hte game
        ui = GetNode<Node2D>("UI");
        menu = GetNode<Node2D>("Menu");
        winScreen = GetNode<Node2D>("WinScreen");
        DeathScreen = GetNode<Node2D>("DeathScreen");
        //Hide all the screens for the game
        HideAll();
        //Show the menu screen at startup
        ShowMenu();
    }

    private void HideAll()
    {
        ui.Hide();
        menu.Hide();
        winScreen.Hide();
        DeathScreen.Hide();
    }

    private void ShowMenu()
    {
        HideAll();
        menu.Show();
        SendUIEvent suiei = new SendUIEvent();
        suiei.showMenu = true;
        suiei.FireEvent();
    }

    private void ShowUI()
    {
        HideAll();
        ui.Show();
        SendUIEvent suiei = new SendUIEvent();
        suiei.showUI = true;
        suiei.startGame = true;
        suiei.FireEvent();
    }

    private void ShowDeathScreen(DeathEvent dei)
    {
        if (dei.target.IsInGroup("Player"))
        {
            HideAll();
            DeathScreen.Show();
            SendUIEvent suiei = new SendUIEvent();
            suiei.showDeathScreen = true;
            suiei.FireEvent();
        }

    }

    private void ShowWinScreen(WinEvent wei)
    {
        HideAll();
        winScreen.Show();
        SendUIEvent suiei = new SendUIEvent();
        suiei.showWinScreen = true;
        suiei.FireEvent();
    }

    public override void _ExitTree()
    {
        WinEvent.UnregisterListener(ShowWinScreen);
        DeathEvent.UnregisterListener(ShowDeathScreen);
    }
}
