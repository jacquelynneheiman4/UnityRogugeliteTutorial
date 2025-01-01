using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int dungeonWidth = 50;
    public int dungeonHeight = 30;
    public int numRooms = 10;
    public int minRoomSize = 3;
    public int maxRoomSize = 7;
    public DungeonVisualizer dungeonVisualizer;

    public void GenerateDungeon()
    {
        Dungeon dungeon = new Dungeon(dungeonWidth, dungeonHeight, numRooms, minRoomSize, maxRoomSize);
        List<Room> rooms = GenerateRooms(dungeon);

        ConnectRooms(dungeon, rooms);
        SelectStartAndExitRooms(dungeon, rooms);

        dungeonVisualizer.Clear();
        dungeonVisualizer.PaintFloorTiles(dungeon);
    }

    public Dungeon CreateDungeon()
    {
        Dungeon dungeon = new Dungeon(dungeonWidth, dungeonHeight, numRooms, minRoomSize, maxRoomSize);
        List<Room> rooms = GenerateRooms(dungeon);

        ConnectRooms(dungeon, rooms);
        SelectStartAndExitRooms(dungeon, rooms);

        dungeonVisualizer.Clear();
        dungeonVisualizer.PaintFloorTiles(dungeon);

        return dungeon;
    }

    private List<Room> GenerateRooms(Dungeon dungeon)
    {
        List<Room> rooms = new List<Room>();
        System.Random random = new System.Random();

        for (int i = 0; i < numRooms; i++)
        {
            for (int attempt = 0; attempt < 100; attempt++)
            {
                int roomWidth = random.Next(minRoomSize, maxRoomSize);
                int roomHeight = random.Next(minRoomSize, maxRoomSize);
                int roomX = random.Next(2, dungeonWidth - roomWidth - 2);
                int roomY = random.Next(2, dungeonHeight - roomHeight - 2);

                Room newRoom = new Room(roomX, roomY, roomWidth, roomHeight);

                bool intersects = false;

                foreach (var room in rooms)
                {
                    if (newRoom.Intersects(room))
                    {
                        intersects = true;
                        break;
                    }
                }

                if (!intersects)
                {
                    rooms.Add(newRoom);
                    dungeon.AddRoom(newRoom);

                    break;
                }
            }
        }

        return rooms;
    }

    private void ConnectRooms(Dungeon dungeon, List<Room> rooms)
    {
        System.Random random = new System.Random();

        for (int i = 1; i < rooms.Count; i++)
        {
            var (x1, y1) = rooms[i - 1].Center();
            var (x2, y2) = rooms[i].Center();

            if(random.Next(0, 2) == 0)
            {
                CreateHorizontalCorridor(dungeon, x1, x2, y1);
                CreateVerticalCorridor(dungeon, y1, y2, x2);
            }
            else
            {
                CreateVerticalCorridor(dungeon, y1, y2, x2);
                CreateHorizontalCorridor(dungeon, x1, x2, y1);
            }

            dungeon.ConnectRooms(rooms[i - 1], rooms[i]);
        }
    }

    private void CreateHorizontalCorridor(Dungeon dungeon, int x1, int x2, int y)
    {
        int start = Math.Min(x1, x2);
        int end = Math.Max(x1, x2);

        for (int i = start; i <= end; i++)
        {
            dungeon.AddFloor(i, y);
        }
    }

    private void CreateVerticalCorridor(Dungeon dungeon, int y1, int y2, int x)
    {
        int start = Math.Min(y1, y2);
        int end = Math.Max(y1, y2);

        for (int i = start; i <= end; i++)
        {
            dungeon.AddFloor(x, i);
        }
    }

    private void SelectStartAndExitRooms(Dungeon dungeon, List<Room> rooms)
    {
        System.Random random = new System.Random();

        Room startRoom = rooms[random.Next(0, rooms.Count)];
        Room exitRoom = SelectExitRoom(startRoom, rooms);

        dungeon.SetStartingLocation(startRoom.Center().x, startRoom.Center().y);
        MarkExitDoor(dungeon, exitRoom);
    }

    private Room SelectExitRoom(Room startRoom, List<Room> rooms)
    {
        System.Random random = new System.Random();
        List<Room> possibleRooms = new List<Room>();

        foreach (var room in rooms)
        {
            if (room != startRoom)
            {
                List<Room> connectedRooms = room.GetConnectedRooms();

                if (!connectedRooms.Contains(startRoom))
                {
                    possibleRooms.Add(room);
                }
            }
        }

        if (possibleRooms.Count == 0)
        {
            possibleRooms = new List<Room>(rooms);
            possibleRooms.Remove(startRoom);

            if (possibleRooms.Count == 0)
            {
                possibleRooms.Add(startRoom);
            }
        }

        return possibleRooms[random.Next(0, possibleRooms.Count)];
    }

    private void MarkExitDoor(Dungeon dungeon, Room room)
    {
        List<(int x, int y)> wallPositions = new List<(int x, int y)>();

        for (int x = room.x - 1; x <= room.x + room.width; x++)
        {
            for (int y = room.y - 1; y <= room.y + room.height; y++)
            {
                if (dungeon.IsWall(x, y) && IsCenterWall(dungeon, x, y))
                {
                    wallPositions.Add((x, y));
                }
            }
        }

        if (wallPositions.Count > 0)
        {
            System.Random random = new System.Random();
            var (x, y) = wallPositions[random.Next(0, wallPositions.Count)];
            dungeon.SetExitLocation(x, y);
        }
    }

    private bool IsCenterWall(Dungeon dungeon, int x, int y)
    {
        string binaryTileType = "";

        foreach (var direction in dungeonVisualizer.wallGenerator.directions)
        {
            int neighborX = x + direction.x;
            int neighborY = y + direction.y;

            if (dungeon.IsInBounds(neighborX, neighborY))
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

        int tileType = Convert.ToInt32(binaryTileType, 2);

        return (
            WallTypesHelper.topWall.Contains(tileType) ||
            WallTypesHelper.rightWall.Contains(tileType) ||
            WallTypesHelper.bottomWall.Contains(tileType) ||
            WallTypesHelper.leftWall.Contains(tileType)
        );
    }
}
