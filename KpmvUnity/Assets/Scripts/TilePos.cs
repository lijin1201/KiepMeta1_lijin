using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePos
{
    public const float BlockAtlasSize = 4.0f;
    int xPos, yPos;

    Vector2[] uvs;

    public TilePos(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        uvs = new Vector2[]
        {
            new Vector2(xPos/BlockAtlasSize + .001f, yPos/BlockAtlasSize + .001f),
            new Vector2(xPos/BlockAtlasSize+ .001f, (yPos+1)/BlockAtlasSize - .001f),
            new Vector2((xPos+1)/BlockAtlasSize - .001f, (yPos+1)/BlockAtlasSize - .001f),
            new Vector2((xPos+1)/BlockAtlasSize - .001f, yPos/BlockAtlasSize+ .001f),
        };
    }

    public Vector2[] GetUVs()
    {
        return uvs;
    }


    public static Dictionary<Tile, TilePos> tiles = new Dictionary<Tile, TilePos>()
    {
        {Tile.Dirt, new TilePos(1,3)},
        {Tile.Grass, new TilePos(3,2)},
        {Tile.GrassSide, new TilePos(2,3)},
        {Tile.Stone, new TilePos(0,3)},
        {Tile.Brick, new TilePos(3,1)},
    };
}

public enum Tile { Dirt, Grass, GrassSide, Stone, TreeSide, TreeCX, Leaves, Brick}