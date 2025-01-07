class Environment
{
    //constants
    private const char roadStrip = '║';
    private const int lanePadding = 5;
    private const int laneSpacing = 20;

    public Environment(ScreenBuffer screenBuffer)
    {
        this.drawRoad(screenBuffer);
    }

    //returns targeted lane offsets
    public static int[] getLaneOffsets()
    {
        int middleLaneOffset = Utilities.screenWidth / 2;
        int[] laneOffsets = { middleLaneOffset - laneSpacing, middleLaneOffset, middleLaneOffset + laneSpacing,
            middleLaneOffset + laneSpacing * 2 };

        return laneOffsets;
    }

    private void drawRoad(ScreenBuffer screenBuffer)
    {
        int[] laneOffsets = getLaneOffsets();

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

