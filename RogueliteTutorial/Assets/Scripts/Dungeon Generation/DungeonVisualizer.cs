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
    public TileBase startTile;

    [Header("Wall Generator")]
    public WallGenerator wallGenerator;

    public void PaintFloorTiles(Dungeon dungeon, bool generateWalls = true)
    {
        char[,] dungeonLayout = dungeon.GetLayout();

        for (int x = 0; x < dungeonLayout.GetLength(0); x++)
        {
            for (int y = 0; y < dungeonLayout.GetLength(1); y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if (dungeon.IsFloor(x, y))
                {
                    PaintTile(tilePosition, floorTilemap, floorTile);
                }
                else if (dungeon.IsStart(x, y))
                {
                    PaintTile(tilePosition, floorTilemap, startTile);
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