using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonVisualizer : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;

    [Header("Floor Tiles")]
    public TileBase floorTile;

    [Header("Wall Generator")]
    public WallGenerator wallGenerator;

    public void PaintFloorTiles(char[,] dungeon, bool generateWalls = true)
    {
        for (int x = 0; x < dungeon.GetLength(0); x++)
        {
            for (int y = 0; y < dungeon.GetLength(1); y++)
            {
                if (dungeon[x, y] == '.')
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    PaintTile(tilePosition, floorTilemap, floorTile);
                }
                
            }
        }

        if (generateWalls)
        {
            wallGenerator.GenerateWalls(dungeon, this);
        }
    }

    public void PaintFloorTiles(HashSet<Vector2Int> floorPositions, bool generateWalls = true)
    {
        foreach (var position in floorPositions)
        {
            PaintTile((Vector3Int)position, floorTilemap, floorTile);
        }

        if (generateWalls)
        {
            wallGenerator.GenerateWalls(floorPositions, this);
        }
    }

    private void PaintTile(Vector3Int position, Tilemap tilemap, TileBase tile)
    {
        tilemap.SetTile(position, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    public void PaintWallTile(Vector2Int position, TileBase wallTile)
    {
        PaintTile((Vector3Int)position, wallTilemap, wallTile);
    }
}