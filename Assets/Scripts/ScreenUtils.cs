using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenUtils
{
    static float screenTop, screenBottom, screenLeft, screenRight;
    static int screenWidth, screenHeight;

    public static float ScreenTop { get{ return screenTop; } }
    public static float ScreenBottom { get { return screenBottom; } }
    public static float ScreenRight
    {
        get
        {
            return screenRight;
        }
    }
    public static float ScreenLeft
    {
        get
        {
            return screenLeft;
        }
    }
    public static void initialize()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);

        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        screenRight = upperRightCornerWorld.x;
        screenLeft = lowerLeftCornerWorld.x;
        screenBottom = lowerLeftCornerWorld.y;
        screenTop = upperRightCornerWorld.y;
    }

    public static void checkScreenSizeChange()
    {
        if (screenWidth != Screen.width || screenHeight != Screen.height)
            initialize();
    }
}
