using Godot;
using System;
using EventCallback;

public class Main : Node2D
{
    //The packed scene for the player that will be instanced later
    PackedScene playerScene = new PackedScene();
    //The node for the player that will be set to the instanced instance of the players packed scene
    Node player;
    //The packed scene for the map that will be instanced later
    PackedScene mapScene = new PackedScene();
    //The node for the map that will be set to the instanced instance of the map packed scene
    Node map;
        //The packed scene for the map that will be instanced later
    PackedScene enemyScene = new PackedScene();
    //The node for the map that will be set to the instanced instance of the map packed scene
    Node enemy;
            //The packed scene for the lighting on the map that will be instanced later
    PackedScene lightingScene = new PackedScene();
    //The node for the lighting that will be set to the instanced instance of the map packed scene
    Node lighting;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Pre load the scenes for the game
        playerScene = ResourceLoader.Load("res://Scenes/Player.tscn") as PackedScene;
        mapScene = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;
        enemyScene = ResourceLoader.Load("res://Scenes/Enemy.tscn") as PackedScene;
        lightingScene = ResourceLoader.Load("res://Scenes/Lighting.tscn") as PackedScene;
        SpawnScenes();
    }

    private void SpawnScenes()
    {
        //----------------------------------
        //NOTE: The scenes instanced first will be at the bottom of the z sorting layer!!!!!!!!
        //----------------------------------

        //Instacne the map
        map = mapScene.Instance();
        map.Name = "Map";
        //Set the map as a child of the main scene
        AddChild(map);
        //Instanciate the player object
        player = playerScene.Instance();
        //Set the name of the player
        player.Name = "Player";
        //Set the position of the player at spawn
        ((Node2D)player).Position = new Vector2(128, 128);
        //Set the player as a child of the main scene
        AddChild(player);
        //Instance the enemy - need to spawn multiple enemies, needs to be done later
        enemy = enemyScene.Instance();
        enemy.Name = "Enemy";
        ((Node2D)enemy).Position = new Vector2(1120, 260);
        AddChild(enemy);
        //Instance the lighting shader scene
        lighting = lightingScene.Instance();
        //Addd the lighting scene as a child of the main sccene node
        AddChild(lighting);

    }
}
