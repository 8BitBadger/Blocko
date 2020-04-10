using Godot;
using System;
using System.Collections.Generic;
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
    //A list of enemies
    List<Node> enemyList = new List<Node>();
    //The packed scene for the ui that will be instanced later
    PackedScene uiScene = new PackedScene();
    //The node for the ui that will be set to the instanced instance of the ui packed scene
    Node ui;
    //The tilemap and map size used for spawning the player and enemies
    TileMap tileMap;
    Vector2 mapSize;

    Vector2[] largestArtifactArea;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Pre load the scenes for the game
        playerScene = ResourceLoader.Load("res://Scenes/Player.tscn") as PackedScene;
        mapScene = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;
        enemyScene = ResourceLoader.Load("res://Scenes/Enemy.tscn") as PackedScene;
        uiScene = ResourceLoader.Load("res://Scenes/UI.tscn") as PackedScene;
        //The UI of the game
        ui = uiScene.Instance();
        AddChild(ui);

        SendUIEvent.RegisterListener(StartGame);
        DeathEvent.RegisterListener(LoseGame);
    }

    private void StartGame(SendUIEvent suiei)
    {
        if (suiei.startGame)
        {
            SpawnScenes();
        }
    }

    private void WinGame()
    {
        ClearScenes();
    }

    private void LoseGame(DeathEvent dei)
    {
        if (dei.target.IsInGroup("Player"))
        {
            ClearScenes();
        }
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
        //Grab the map info for the spawning of the player
        MapInfoRequestEvent mirei = new MapInfoRequestEvent();
        mirei.FireEvent();
        tileMap = mirei.tileMap;
        mapSize = mirei.mapSize;
        //Spawn the player in an open tile somewhere in the left uper corner of the map
        SpawnPlayer();
        //Spawn enemies across the map
        SpawnEnemies();
    }

    private void ClearScenes()
    {
        map.QueueFree();

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] != null) enemyList[i].QueueFree();
        }
        player.QueueFree();
    }

    private void SpawnPlayer()
    {
        bool playerPlaced = false;
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                if (tileMap.GetCell(x, y) == 0 && !playerPlaced)
                {
                    //Instanciate the player object
                    player = playerScene.Instance();
                    //Set the name of the player
                    player.Name = "Player";
                    //Set the position of the player at spawn
                    ((Node2D)player).Position = new Vector2(x * 32, y * 32);
                    //Set the player as a child of the main scene
                    AddChild(player);
                    playerPlaced = true;
                    break;
                }
            }
        }
    }
    private void SpawnArtifact(int tileType)
    {
        bool artifactPlaced = false;
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                if (tileMap.GetCell(x, y) == 3)
                {
                    //Instanciate the player object
                    player = playerScene.Instance();
                    //Set the name of the player
                    player.Name = "Player";
                    //Set the position of the player at spawn
                    ((Node2D)player).Position = new Vector2(x * 32, y * 32);
                    //Set the player as a child of the main scene
                    AddChild(player);
                    artifactPlaced = true;
                    break;
                }
            }
        }

    }

    private void GetArea(Vector2 origen, int tileType)
    {
        List<Vector2> scanList = new List<Vector2>();
        List<Vector2> scanedList = new List<Vector2>();

        scanList.Add(origen);
While(scanList.Count > 0)
{
        for (int i = 0; i < scanList.Count; i++)
        {
            for (int y = (int)(scanList[i].y - 1); y < (int)(scanList[i].y + 1); y++)
            {
                for (int x = (int)(scanList[i].x - 1); x < (int)(scanList[i].y + 1); x++)
                {
                    if (tileMap.GetCell(x, y) == tileType && scanList[i] != new Vector2(x, y))
                    {
bool scanlistContains = false;
for (int h = 0; h < scanList.Count; h++) 
{
if(scanList[h] == new Vector2(x, y) scanlistContains = true;
} 
if(!scanlistContains) scanList.Add(new Vector2(x, y));
                    }
                }
            }
            scanedList.Add(scanList[i]);
            scanList.RemoveAt(i);
        }
}
        //Add the origen tile to a scan list
        //Get the neighbours of all the tiles inside the scan list and add them to the scan list if they are of the same type and are not on the scaned or scan list
        //Then move the origen tile to the scaned list
        //Repeat above code until scan list is comepletly empty



    }

    private void SpawnEnemies()
    {
        //The amount of enemies we want to spawn
        int AmountOfEnemies = 20;
        //If the position on the tile map is open
        bool gotPos = false;
        //The random number generator used for the selection of the position
        RandomNumberGenerator rand = new RandomNumberGenerator();
        //Runn throught the loop the amount of times we want to spawn an enemy
        for (int i = 0; i < AmountOfEnemies; i++)
        {
            gotPos = false;
            //Run the loop until we find a open spot where the enemy can be spawned
            while (!gotPos)
            {
                //Randomize the generator every loop to make sure we get random placement
                rand.Randomize();
                //Generate the x and y positions
                int xPos = (int)rand.RandiRange(0, (int)mapSize.x);
                int yPos = (int)rand.RandiRange(0, (int)mapSize.y);
                //Check if the map tile is a floor
                if (tileMap.GetCell(xPos, yPos) == 0)
                {
                    gotPos = true;
                    PackedScene tempEnemy = new PackedScene();
                    tempEnemy = enemyScene.Duplicate(true) as PackedScene;
                    //Instance the enemy - need to spawn multiple enemies, needs to be done later
                    enemy = tempEnemy.Instance();
                    enemy.Name = "Enemy";
                    ((Node2D)enemy).Position = new Vector2(xPos * 32, yPos * 32);
                    AddChild(enemy);
                    enemyList.Add(enemy);
                }
            }
        }
    }
    public override void _ExitTree()
    {
        SendUIEvent.UnregisterListener(StartGame);
        DeathEvent.UnregisterListener(LoseGame);
    }
}
