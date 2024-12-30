using UnityEngine;
using System.Collections.Generic;

public class BSP_DungeonGenerator : MonoBehaviour
{
    [Header("Dungeon Settings")]
    public int width = 50;
    public int height = 50;
    public int minRoomWidth = 5;
    public int minRoomHeight = 5;
    public int minRoomCount = 5;
    public int maxRoomCount = 10;
    public int offset = 2;
    public DungeonVisualizer dungeonVisualizer;

    private int targetRoomCount;

    public void GenerateDungeon()
    {
        dungeonVisualizer.Clear();

        targetRoomCount = Random.Range(minRoomCount, maxRoomCount);

        BoundsInt dungeonBounds = new BoundsInt(
            Vector3Int.zero,
            new Vector3Int(width, height, 0)
        );

        List<BoundsInt> rooms = GenerateRooms(dungeonBounds);
        HashSet<Vector2Int> floorPositions = CreateRooms(rooms);

        List<Vector2Int> roomCenters = new List<Vector2Int>();

        foreach (var room in rooms)
        {
            Vector3Int roomCenter = Vector3Int.RoundToInt(room.center);
            roomCenters.Add((Vector2Int)roomCenter);
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floorPositions.UnionWith(corridors);

        dungeonVisualizer.PaintFloorTiles(floorPositions);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();

        List<Vector2Int> connectedRooms = new List<Vector2Int>();
        List<Vector2Int> unconnectedRooms = new List<Vector2Int>(roomCenters);

        unconnectedRooms.Shuffle();

        connectedRooms.Add(unconnectedRooms[0]);
        unconnectedRooms.RemoveAt(0);

        while(unconnectedRooms.Count > 0)
        {
            Vector2Int currentRoom = unconnectedRooms[0];
            unconnectedRooms.RemoveAt(0);

            Vector2Int closestConnectedRoom = FindClosestRoom(currentRoom, connectedRooms);
            corridors.UnionWith(CreateCorridor(closestConnectedRoom, currentRoom));

            int additionalConnections = Random.Range(0, 3);

            if (additionalConnections > 0 && connectedRooms.Count > 1)
            {
                for (int i = 0; i < additionalConnections; i++)
                {
                    Vector2Int randomRoom = connectedRooms[Random.Range(0, connectedRooms.Count)];

                    if (randomRoom != closestConnectedRoom)
                    {
                        corridors.UnionWith(CreateCorridor(randomRoom, currentRoom));
                    }
                }
            }

            connectedRooms.Add(currentRoom);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int closestConnectedRoom, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();

        Vector2Int position = closestConnectedRoom;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
        }

        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestRoom(Vector2Int currentRoom, List<Vector2Int> connectedRooms)
    {
        Vector2Int closestRoom = connectedRooms[0];
        float shortestDistance = float.MaxValue;

        foreach (var room in connectedRooms)
        {
            float distance = Vector2Int.Distance(currentRoom, room);

            if (distance < shortestDistance)
            {
                closestRoom = room;
                shortestDistance = distance;
            }
        }

        return closestRoom;
    }

    private List<BoundsInt> GenerateRooms(BoundsInt dungeonBounds)
    {
        List<BoundsInt> rooms = new List<BoundsInt>();

        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        roomsQueue.Enqueue(dungeonBounds);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();

            if (room.size.x >= minRoomWidth * 2 || room.size.y >= minRoomHeight * 2)
            {
                if(Random.value < 0.5f)
                {
                    if (room.size.y >= minRoomHeight * 2)
                    {
                        SplitHorizontally(roomsQueue, room);
                    }
                    else
                    {
                        SplitVertically(roomsQueue, room);
                    }
                }
                else
                {
                    if (room.size.x >= minRoomWidth * 2)
                    {
                        SplitVertically(roomsQueue, room);
                    }
                    else
                    {
                        SplitHorizontally(roomsQueue, room);
                    }
                }
            }
            else 
            {
                if (rooms.Count < targetRoomCount)
                {
                    rooms.Add(room);
                }
                else
                {
                    break;
                }
            }
        }

        return rooms;
    }

    private void SplitVertically(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int xSplit = Random.Range(1, room.size.x / 2) + room.size.x / 4;

        BoundsInt room1 = new BoundsInt(
            room.min,
            new Vector3Int(xSplit, room.size.y, room.size.z)
        );

        BoundsInt room2 = new BoundsInt(
            new Vector3Int(xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z)
        );

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private void SplitHorizontally(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int ySplit = Random.Range(1, room.size.y / 2) + room.size.y / 4;

        BoundsInt room1 = new BoundsInt(
            room.min,
            new Vector3Int(room.size.x, ySplit, room.size.z)
        );

        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x, ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z)
        );

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private HashSet<Vector2Int> CreateRooms(List<BoundsInt> rooms)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        foreach (var room in rooms)
        {
            for (int x = offset; x < room.size.x - offset; x++)
            {
                for (int y = offset; y < room.size.y - offset; y++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(x, y);
                    floorPositions.Add(position);
                }
            }
        }

        return floorPositions;
    }
}
