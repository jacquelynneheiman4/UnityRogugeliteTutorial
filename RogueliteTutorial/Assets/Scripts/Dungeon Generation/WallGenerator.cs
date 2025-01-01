using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class WallGenerator
{
    [Header("Default Tiles")]
    public TileBase defaultWallTile;

    [Header("Exit Tiles")]
    public TileBase topExitTile;
    public TileBase rightExitTile;
    public TileBase bottomExitTile;
    public TileBase leftExitTile;

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
    public TileBase rightWallInnerEndTopTile;
    public TileBase rightWallInnerEndBottomTile;
    public TileBase rightWallInnerCenterTile;
    public TileBase topWallTTile;
    public TileBase bottomWallTTile;
    public TileBase rightWallTTile;
    public TileBase leftWallTTile;
    public TileBase topRightConnectionLeftBorderTile;
    public TileBase topRightConnectionBottomBorderTile;
    public TileBase topLeftConnectionRightBorderTile;
    public TileBase topLeftConnectionBottomBorderTile;
    public TileBase bottomLeftConnectionTopBorderTile;
    public TileBase bottomRightConnectionTopBorderTile;
    public TileBase bottomRightConnectionLeftBorderTile;
    public TileBase bottomLeftConnectionRightBorderTile;
    public TileBase topWallInnerCenterTile;
    public TileBase topWallInnerEndLeftTile;
    public TileBase topWallInnerEndRightTile;
    public TileBase topRightInteriorCornerTile;
    public TileBase topLeftInteriorCornerTile;
    public TileBase bottomRightInteriorCornerTile;
    public TileBase bottomLeftInteriorCornerTile;
    public TileBase bottomLeftTopRightConnectionTile;
    public TileBase topLeftBottomRightConnectionTile;
    public TileBase topRightConnectionTile;
    public TileBase topLeftConnectionTile;
    public TileBase bottomRightConnectionTile;
    public TileBase bottomLeftConnectionTile;
    public TileBase singleTile;
    public TileBase bottomInteriorTTile;
    public TileBase topInteriorTTile;
    public TileBase leftInteriorTTile;
    public TileBase rightInteriorTTile;

    public List<Vector2Int> directions = new List<Vector2Int>
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

    public void GenerateWalls(Dungeon dungeon, DungeonVisualizer dungeonVisualizer)
    {
        char[,] dungeonLayout = dungeon.GetLayout();

        int dungeonWidth = dungeonLayout.GetLength(0);
        int dungeonHeight = dungeonLayout.GetLength(1);

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
                        if (dungeon.IsFloor(neighborX, neighborY))
                        {
                            binaryTileType += "1";
                        }
                        else 
                        {
                            binaryTileType += "0";
                        }
                    }
                }

                Vector2Int tilePosition = new Vector2Int(x, y);

                if (dungeon.IsWall(x, y))
                {
                    TileBase tile = GetWallTile(binaryTileType);
                    dungeonVisualizer.PaintWallTile(tilePosition, tile);
                }
                else if (dungeon.IsExit(x, y))
                {
                    TileBase tile = GetExitTile(binaryTileType);
                    dungeonVisualizer.PaintWallTile(tilePosition, tile);
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

    private TileBase GetExitTile(string binaryTileType)
    {
        int tileType = Convert.ToInt32(binaryTileType, 2);

        if (ExitTypesHelper.topExit.Contains(tileType))
        {
            return topExitTile;
        }
        else if (ExitTypesHelper.rightExit.Contains(tileType))
        {
            return rightExitTile;
        }
        else if (ExitTypesHelper.bottomExit.Contains(tileType))
        {
            return bottomExitTile;
        }
        else if (ExitTypesHelper.leftExit.Contains(tileType))
        {
            return leftExitTile;
        }

        return null;
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
        else if (WallTypesHelper.rightWallInnerEndTop.Contains(tileType))
        {
            return rightWallInnerEndTopTile;
        }
        else if (WallTypesHelper.rightWallInnerEndBottom.Contains(tileType))
        {
            return rightWallInnerEndBottomTile;
        }
        else if (WallTypesHelper.rightWallInnerCenter.Contains(tileType))
        {
            return rightWallInnerCenterTile;
        }
        else if (WallTypesHelper.topWallT.Contains(tileType))
        {
            return topWallTTile;
        }
        else if (WallTypesHelper.bottomWallT.Contains(tileType))
        {
            return bottomWallTTile;
        }
        else if (WallTypesHelper.rightWallT.Contains(tileType))
        {
            return rightWallTTile;
        }
        else if (WallTypesHelper.leftWallT.Contains(tileType))
        {
            return leftWallTTile;
        }
        else if (WallTypesHelper.topRightConnectionLeftBorder.Contains(tileType))
        {
            return topRightConnectionLeftBorderTile;
        }
        else if (WallTypesHelper.topRightConnectionBottomBorder.Contains(tileType))
        {
            return topRightConnectionBottomBorderTile;
        }
        else if (WallTypesHelper.topLeftConnectionRightBorder.Contains(tileType))
        {
            return topLeftConnectionRightBorderTile;
        }
        else if (WallTypesHelper.topLeftConnectionBottomBorder.Contains(tileType))
        {
            return topLeftConnectionBottomBorderTile;
        }
        else if (WallTypesHelper.bottomLeftConnectionTopBorder.Contains(tileType))
        {
            return bottomLeftConnectionTopBorderTile;
        }
        else if (WallTypesHelper.bottomRightConnectionTopBorder.Contains(tileType))
        {
            return bottomRightConnectionTopBorderTile;
        }
        else if (WallTypesHelper.bottomRightConnectionLeftBorder.Contains(tileType))
        {
            return bottomRightConnectionLeftBorderTile;
        }
        else if (WallTypesHelper.bottomLeftConnectionRightBorder.Contains(tileType))
        {
            return bottomLeftConnectionRightBorderTile;
        }
        else if (WallTypesHelper.topWallInnerCenter.Contains(tileType))
        {
            return topWallInnerCenterTile;
        }
        else if (WallTypesHelper.topWallInnerEndLeft.Contains(tileType))
        {
            return topWallInnerEndLeftTile;
        }
        else if (WallTypesHelper.topWallInnerEndRight.Contains(tileType))
        {
            return topWallInnerEndRightTile;
        }
        else if (WallTypesHelper.topRightInteriorCorner.Contains(tileType))
        {
            return topRightInteriorCornerTile;
        }
        else if (WallTypesHelper.topLeftInteriorCorner.Contains(tileType))
        {
            return topLeftInteriorCornerTile;
        }
        else if (WallTypesHelper.bottomRightInteriorCorner.Contains(tileType))
        {
            return bottomRightInteriorCornerTile;
        }
        else if (WallTypesHelper.bottomLeftInteriorCorner.Contains(tileType))
        {
            return bottomLeftInteriorCornerTile;
        }
        else if (WallTypesHelper.bottomLeftTopRightConnection.Contains(tileType))
        {
            return bottomLeftTopRightConnectionTile;
        }
        else if (WallTypesHelper.topLeftBottomRightConnection.Contains(tileType))
        {
            return topLeftBottomRightConnectionTile;
        }
        else if (WallTypesHelper.topRightConnection.Contains(tileType))
        {
            return topRightConnectionTile;
        }
        else if (WallTypesHelper.topLeftConnection.Contains(tileType))
        {
            return topLeftConnectionTile;
        }
        else if (WallTypesHelper.bottomRightConnection.Contains(tileType))
        {
            return bottomRightConnectionTile;
        }
        else if (WallTypesHelper.bottomLeftConnection.Contains(tileType))
        {
            return bottomLeftConnectionTile;
        }
        else if (WallTypesHelper.single.Contains(tileType))
        {
            return singleTile;
        }
        else if (WallTypesHelper.bottomInteriorT.Contains(tileType))
        {
            return bottomInteriorTTile;
        }
        else if (WallTypesHelper.topInteriorT.Contains(tileType))
        {
            return topInteriorTTile;
        }
        else if (WallTypesHelper.leftInteriorT.Contains(tileType))
        {
            return leftInteriorTTile;
        }
        else if (WallTypesHelper.rightInteriorT.Contains(tileType))
        {
            return rightInteriorTTile;
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
