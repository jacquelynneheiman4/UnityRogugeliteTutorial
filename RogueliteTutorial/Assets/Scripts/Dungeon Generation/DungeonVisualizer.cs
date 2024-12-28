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


    public void PaintFloorTiles(HashSet<Vector2Int> floorPositions, bool generateWalls = true)
    {
        foreach (var position in floorPositions)
        {
            PaintTile(position, floorTilemap, floorTile);
        }

        if (generateWalls)
        {
            wallGenerator.GenerateWalls(floorPositions, this);
        }
    }

    private void PaintTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    public void PaintWallTile(Vector2Int position, TileBase wallTile)
    {
        PaintTile(position, wallTilemap, wallTile);
    }
}