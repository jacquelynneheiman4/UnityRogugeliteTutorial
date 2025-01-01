using System.Collections.Generic;

public static class ExitTypesHelper
{
    public static HashSet<int> topExit = new HashSet<int>
    {
        0b00011000,
        0b00011100,
        0b00001100,
    };

    public static HashSet<int> leftExit = new HashSet<int>
    {
        0b00110000,
        0b01110000,
        0b01100000,
    };

    public static HashSet<int> bottomExit = new HashSet<int>
    {
        0b11000000,
        0b11000001,
        0b10000001,
    };

    public static HashSet<int> rightExit = new HashSet<int>
    {
        0b00000011,
        0b00000111,
        0b00000110,
    };
}