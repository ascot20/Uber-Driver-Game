class Environment
{
    //constants
    private const char roadStrip = '║';
    private const int lanePadding = 5;
    private const int laneSpacing = 20;
    private const int laneMultiplier = 2;
    const int firstRowPos = 0;

    public Environment(ScreenBuffer screenBuffer)
    {
        this.drawEnvironment(screenBuffer);
    }

    //returns targeted lane offsets
    public static int[] getLanePositions()
    {
        int middleLanePosition = Utilities.screenWidth / 2;
        int[] lanePositions = { middleLanePosition - laneSpacing, middleLanePosition, middleLanePosition + laneSpacing,
            middleLanePosition + laneSpacing * laneMultiplier };

        return lanePositions;
    }

    public void drawEnvironment(ScreenBuffer screenBuffer)
    {
        this.drawRoad(screenBuffer);
        this.writeGameInstructions(screenBuffer);
    }

    private void drawRoad(ScreenBuffer screenBuffer)
    {
        int[] lanePositons = getLanePositions();

        for (int i = 0; i < lanePositons.Length; i++)
        {
            for (int j = 0; j < Utilities.screenHeight; j++)
            {
                BufferChar c;
                c.xPos = lanePositons[i] - lanePadding;
                c.yPos = j;
                c.character = roadStrip;

                screenBuffer.writeChar(c);
            }
        }
    }

    private void writeGameInstructions(ScreenBuffer screenBuffer)
    {
        BufferString instructions = Utilities.createLeftAlignedBufferString("Use A and D to steer car left or right.", firstRowPos);
        screenBuffer.writeLine(instructions);
    }
}

