using System;
using UnityEngine;

public static class PlayerColors
{
    enum playercolors {Blue, Green, Red, Yellow}

    public static Color ToColor(int i)
    {
        switch (i)
        {
            case 0:
                return Color.blue;
            case 1:
                return Color.green;
            case 2:
                return Color.red;
            case 3:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    public static String ToString(int i)
    {
        switch (i)
        {
            case 0:
                return "Blue";
            case 1:
                return "Green";
            case 2:
                return "Red";
            case 3:
                return "Yellow";
            default:
                return "N/A";
        }
    }

    public static String ToStringLower(int i)
    {
        return ToString(i).ToLower();
    }
}
