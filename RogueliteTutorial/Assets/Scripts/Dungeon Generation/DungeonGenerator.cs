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

    private char wallChar = '#';
    private char floorChar = '.';

    public void GenerateDungeon()
    {
        char[,] dungeon = InitializeDungeon();
        List<Room> rooms = GenerateRooms(dungeon);

        ConnectRooms(dungeon, rooms);

        dungeonVisualizer.Clear();
        dungeonVisualizer.PaintFloorTiles(dungeon);
    }

    private char[,] InitializeDungeon()
    {
        char[,] dungeon = new char[dungeonWidth, dungeonHeight];

        for (int x = 0; x < dungeonWidth; x++)
        {
            for (int y = 0; y < dungeonHeight; y++)
            {
                dungeon[x, y] = wallChar;
            }
        }

        return dungeon;
    }

    private List<Room> GenerateRooms(char[,] dungeon)
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

                    for (int x = roomX; x < roomX + roomWidth; x++)
                    {
                        for (int y = roomY; y < roomY + roomHeight; y++)
                        {
                            dungeon[x, y] = floorChar;
                        }
                    }

                    break;
                }
            }
        }

        return rooms;
    }

    private void ConnectRooms(char[,] dungeon, List<Room> rooms)
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
        }
    }

    private void CreateHorizontalCorridor(char[,] dungeon, int x1, int x2, int y)
    {
        int start = Math.Min(x1, x2);
        int end = Math.Max(x1, x2);

        for (int i = start; i <= end; i++)
        {
            dungeon[i, y] = floorChar;
        }
    }

    private void CreateVerticalCorridor(char[,] dungeon, int y1, int y2, int x)
    {
        int start = Math.Min(y1, y2);
        int end = Math.Max(y1, y2);

        for (int i = start; i <= end; i++)
        {
            dungeon[x, i] = floorChar;
        }
    }
}
