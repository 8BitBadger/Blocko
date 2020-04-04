using Godot;
using System;
using EventCallback;

public enum TileType
{
    //NOTE: The numbers her should coinside with the TileMap Node from GODOT itself otherwise it wont generate correctly
    NONE = -1,
    FLOOR,
    WALL,
    WALL_BREAKABLE
};
public class Tile
{
    public Vector2 pos;
    public TileType type;
    int health = 3;
}
