using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

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
        0b10110001,
        0b11100000,
    };

    public static HashSet<int> topLeftInnerCorner = new HashSet<int>
    {
        0b11000111,
        0b10000111,
        0b11000011,
        0b11000110,
        0b10000011,
    };

    public static HashSet<int> bottomRightInnerCorner = new HashSet<int>
    {
        0b01111100,
        0b00111100,
        0b01111000,
        0b01111101,
        0b00111000,
        0b01101100,
    };

    public static HashSet<int> bottomLeftInnerCorner = new HashSet<int>
    {
        0b00011111,
        0b00011110,
        0b00001111,
        0b00001110,
        0b00011011,
    };

    public static HashSet<int> rightWallInnerEndTop = new HashSet<int>
    {
        0b11110111,
        0b11110011,
        0b11100111,
        0b11100011,
        0b10110111,
        0b11110110,
    };

    public static HashSet<int> rightWallInnerCenter = new HashSet<int>
    {
        0b01110111,
        0b00110111,
        0b01110110,
        0b00110110,
        0b01100011,
        0b01100111,
        0b01110011,
        0b00110011,
        0b01100110,
        0b01010111,
    };

    public static HashSet<int> rightWallInnerEndBottom = new HashSet<int>
    {
        0b01111111,
        0b00111111,
        0b01101111,
        0b01111110,
        0b00111110,
        0b01111011,
    };

    public static HashSet<int> topWallT = new HashSet<int>
    {
        0b00010100,
    };

    public static HashSet<int> bottomWallT = new HashSet<int>
    {
        0b01000001,
    };

    public static HashSet<int> rightWallT = new HashSet<int>
    {
        0b00000101,
    };

    public static HashSet<int> leftWallT = new HashSet<int>
    {
        0b01010000,
    };

    public static HashSet<int> topRightConnectionLeftBorder = new HashSet<int>
    {
        0b00010111,
        0b00010110,
        0b00010011,
    };

    public static HashSet<int> topRightConnectionBottomBorder = new HashSet<int>
    {
        0b00011101,
        0b00001101,
        0b00011001,
    };

    public static HashSet<int> topLeftConnectionRightBorder = new HashSet<int>
    {
        0b01110100,
        0b00110100,
        0b01100100,
    };

    public static HashSet<int> topLeftConnectionBottomBorder = new HashSet<int>
    {
        0b01011100,
        0b01011000,
        0b01001100,
    };

    public static HashSet<int> bottomLeftConnectionTopBorder = new HashSet<int>
    {
        0b11010001,
        0b11010000,
        0b10010001,
    };

    public static HashSet<int> bottomRightConnectionTopBorder = new HashSet<int>
    {
        0b11000101,
        0b10000101,
        0b11000100,
    };

    public static HashSet<int> bottomRightConnectionLeftBorder = new HashSet<int>
    {
        0b01000111,
        0b01000011,
        0b01000110,
    };

    public static HashSet<int> bottomLeftConnectionRightBorder = new HashSet<int>
    {
        0b01110001,
        0b00110001,
        0b01100001,
    };

    public static HashSet<int> topWallInnerCenter = new HashSet<int>
    {
        0b11011100,
        0b11011101,
        0b10011101,
        0b11001101,
        0b11011001,
        0b10001101,
        0b11011000,
        0b11001100,
        0b10011001,
        0b01011101,
    };

    public static HashSet<int> topWallInnerEndLeft = new HashSet<int>
    {
        0b11011111,
        0b10001111,
        0b11001111,
        0b10011111,
        0b11011011,
        0b11011110,
    };

    public static HashSet<int> topWallInnerEndRight = new HashSet<int>
    {
        0b11111101,
        0b11111001,
        0b11111100,
        0b11111000,
        0b10111101,
        0b11101101,
    };

    public static HashSet<int> topRightInteriorCorner = new HashSet<int>
    {
        0b11110101,
    };

    public static HashSet<int> topLeftInteriorCorner = new HashSet<int>
    {
        0b11010111,
        0b11010011,
    };

    public static HashSet<int> bottomRightInteriorCorner = new HashSet<int>
    {
        0b00111101,
        0b01111001,
    };

    public static HashSet<int> bottomLeftInteriorCorner = new HashSet<int>
    {
        0b01001111,
        0b01011111,
        0b01011110,
    };

    public static HashSet<int> bottomLeftTopRightConnection = new HashSet<int>
    {
        0b00010001,
    };

    public static HashSet<int> topLeftBottomRightConnection = new HashSet<int>
    {
        0b01000100,
    };

    public static HashSet<int> topRightConnection = new HashSet<int>
    {
        0b00010101,
    };

    public static HashSet<int> topLeftConnection = new HashSet<int>
    {

    };

    public static HashSet<int> bottomRightConnection = new HashSet<int>
    {

    };

    public static HashSet<int> bottomLeftConnection = new HashSet<int>
    {

    };

    public static HashSet<int> single = new HashSet<int>
    {
        0b11111111,
        0b11111011,
        0b10111111,
        0b11111110,
    };

    public static HashSet<int> bottomInteriorT = new HashSet<int>
    {
        0b01001101,
    };

    public static HashSet<int> topInteriorT = new HashSet<int>
    {

    };

    public static HashSet<int> leftInteriorT = new HashSet<int>
    {

    };

    public static HashSet<int> rightInteriorT = new HashSet<int>
    {
        0b01110101,
    };
}