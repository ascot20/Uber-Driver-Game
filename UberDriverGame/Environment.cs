using System;

class Environment
{
    //constants
    private const char roadStrip = '║';
    private const int lanePadding = 5;
    private const int laneSpacing = 20;

    //static members
    public static int[] laneOffsets;

    public Environment(ScreenBuffer screenBuffer)
    {
        this.calculateLaneOffsets();
        this.drawRoad(screenBuffer);
    }

    //calculates targeted lane offsets
    private void calculateLaneOffsets()
    {
        int middleLaneOffset = Utilities.screenWidth / 2;
        laneOffsets = new int[] { middleLaneOffset - laneSpacing, middleLaneOffset, middleLaneOffset + laneSpacing,
            middleLaneOffset + laneSpacing * 2 };
    }

    private void drawRoad(ScreenBuffer screenBuffer)
    {
        for (int i = 0; i < laneOffsets.Length; i++)
        {
            for (int j = 0; j < Utilities.screenHeight; j++)
            {
                BufferChar c;
                c.xPos = laneOffsets[i] - lanePadding;
                c.yPos = j;
                c.character = roadStrip;

                screenBuffer.writeChar(c);
            }
        }
    }
}

