using System.Collections.Generic;

public class Room
{
    public int x;
    public int y;
    public int width;
    public int height;

    private List<Room> connectedRooms = new List<Room>();

    public Room(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public (int x, int y) Center()
    {
        return (x + width / 2, y + height / 2);
    }

    public bool Intersects(Room other)
    {
        return x < other.x + other.width && 
            x + width > other.x &&
            y < other.y + other.height &&
            y + height > other.y;
    }

    public void ConnectRoom(Room other)
    {
        if (!connectedRooms.Contains(other))
        {
            connectedRooms.Add(other);
        }
    }

    public List<Room> GetConnectedRooms() => connectedRooms;
}