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

    public void PaintFloorTiles(HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            PaintTile(position, floorTilemap, floorTile);
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
}