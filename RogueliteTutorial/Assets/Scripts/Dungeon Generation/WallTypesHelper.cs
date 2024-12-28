using System.Collections.Generic;

public static class WallTypesHelper
{
    public static HashSet<int> topWall = new HashSet<int>
    {
        0b00011000,
        0b00011100,
        0b00001100,
    };

    public static HashSet<int> leftWall = new HashSet<int>
    {
        0b00110000,
        0b01110000,
        0b01100000,
    };

    public static HashSet<int> bottomWall = new HashSet<int>
    {
        0b11000000,
        0b11000001,
        0b10000001,
    };

    public static HashSet<int> rightWall = new HashSet<int>
    {
        0b00000011,
        0b00000111,
        0b00000110,
    };

    public static HashSet<int> topLeftCorner = new HashSet<int>
    {
        0b00010000,
    };

    public static HashSet<int> topRightCorner = new HashSet<int>
    {
        0b00000100,
    };

    public static HashSet<int> bottomLeftCorner = new HashSet<int>
    {
        0b01000000,
    };

    public static HashSet<int> bottomRightCorner = new HashSet<int>
    {
        0b00000001,
    };

    public static HashSet<int> topRightInnerCorner = new HashSet<int>
    {
        0b11110001,
        0b11110000,
        0b11100001,
    };

    public static HashSet<int> topLeftInnerCorner = new HashSet<int>
    {
        0b11000111,
        0b10000111,
        0b11000011,
    };

    public static HashSet<int> bottomRightInnerCorner = new HashSet<int>
    {
        0b01111100,
        0b00111100,
        0b01111000,
    };

    public static HashSet<int> bottomLeftInnerCorner = new HashSet<int>
    {
        0b00011111,
        0b00011110,
        0b00001111,
    };
}