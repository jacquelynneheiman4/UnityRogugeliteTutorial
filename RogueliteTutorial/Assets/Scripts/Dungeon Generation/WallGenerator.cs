using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class WallGenerator
{
    [Header("Default Tiles")]
    public TileBase defaultWallTile;

    [Header("Tiles")]
    public TileBase topWallTile;
    public TileBase leftWallTile;
    public TileBase bottomWallTile;
    public TileBase rightWallTile;
    public TileBase topLeftCornerTile;
    public TileBase topRightCornerTile;
    public TileBase bottomLeftCornerTile;
    public TileBase bottomRightCornerTile;
    public TileBase topRightInnerCornerTile;
    public TileBase topLeftInnerCornerTile;
    public TileBase bottomRightInnerCornerTile;
    public TileBase bottomLeftInnerCornerTile;

    private List<Vector2Int> directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1),       // Top
        new Vector2Int(1, 1),       // Top Right
        new Vector2Int(1, 0),       // Right
        new Vector2Int(1, -1),       // Bottom Right
        new Vector2Int(0, -1),       // Bottom
        new Vector2Int(-1, -1),       // Bottom Left
        new Vector2Int(-1, 0),       // Left
        new Vector2Int(-1, 1),       // Top Left
    };

    public void GenerateWalls(char[,] dungeon, DungeonVisualizer dungeonVisualizer)
    {
        int dungeonWidth = dungeon.GetLength(0);
        int dungeonHeight = dungeon.GetLength(1);

        for (int x = 0; x < dungeonWidth; x++)
        {
            for (int y = 0; y < dungeonHeight; y++)
            {
                string binaryTileType = "";

                foreach (var direction in directions)
                {
                    int neighborX = x + direction.x;
                    int neighborY = y + direction.y;

                    bool isInBounds = (
                        neighborX >= 0 &&
                        neighborX < dungeonWidth &&
                        neighborY >= 0 &&
                        neighborY < dungeonHeight
                    );

                    if (isInBounds)
                    {
                        if (dungeon[neighborX, neighborY] == '.')
                        {
                            binaryTileType += "1";
                        }
                        else 
                        {
                            binaryTileType += "0";
                        }
                    }
                }

                if (dungeon[x, y] == '#')
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    TileBase tile = GetWallTile(binaryTileType);
                    dungeonVisualizer.PaintWallTile((Vector2Int)tilePosition, tile);
                }
            }
        }
    }

    public void GenerateWalls(HashSet<Vector2Int> floorPositions, DungeonVisualizer dungeonVisualizer)
    {
        HashSet<Vector2Int> wallPositions = FindWallPositions(floorPositions);
        CreateWalls(dungeonVisualizer, wallPositions, floorPositions);
    }

    private void CreateWalls(DungeonVisualizer dungeonVisualizer, HashSet<Vector2Int> wallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in wallPositions)
        {
            string binaryTileType = "";

            foreach (var direction in directions)
            {
                var neighbor = position + direction;

                if (floorPositions.Contains(neighbor))
                {
                    binaryTileType += "1";
                }
                else 
                {
                    binaryTileType += "0";
                }
            }

            TileBase wallTile = GetWallTile(binaryTileType);
            dungeonVisualizer.PaintWallTile(position, wallTile);
        }
    }

    private TileBase GetWallTile(string binaryTileType)
    {
        int tileType = Convert.ToInt32(binaryTileType, 2);

        if (WallTypesHelper.topWall.Contains(tileType))
        {
            return topWallTile;
        }
        else if (WallTypesHelper.leftWall.Contains(tileType))
        {
            return leftWallTile;
        }
        else if (WallTypesHelper.bottomWall.Contains(tileType))
        {
            return bottomWallTile;
        }
        else if (WallTypesHelper.rightWall.Contains(tileType))
        {
            return rightWallTile;
        }
        else if (WallTypesHelper.topLeftCorner.Contains(tileType))
        {
            return topLeftCornerTile;
        }
        else if (WallTypesHelper.topRightCorner.Contains(tileType))
        {
            return topRightCornerTile;
        }
        else if (WallTypesHelper.bottomLeftCorner.Contains(tileType))
        {
            return bottomLeftCornerTile;
        }
        else if (WallTypesHelper.bottomRightCorner.Contains(tileType))
        {
            return bottomRightCornerTile;
        }
        else if (WallTypesHelper.topRightInnerCorner.Contains(tileType))
        {
            return topRightInnerCornerTile;
        }
        else if (WallTypesHelper.topLeftInnerCorner.Contains(tileType))
        {
            return topLeftInnerCornerTile;
        }
        else if (WallTypesHelper.bottomLeftInnerCorner.Contains(tileType))
        {
            return bottomLeftInnerCornerTile;
        }
        else if (WallTypesHelper.bottomRightInnerCorner.Contains(tileType))
        {
            return bottomRightInnerCornerTile;
        }

        return defaultWallTile;
    }

    private HashSet<Vector2Int> FindWallPositions(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directions)
            {
                var neighbor = position + direction;

                if (!floorPositions.Contains(neighbor))
                {
                    wallPositions.Add(neighbor);
                }
            }
        }

        return wallPositions;
    }
}
