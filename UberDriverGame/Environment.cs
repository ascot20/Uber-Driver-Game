using Utilities;

class Environment
{
    private const char roadBorder = '║';
    private const int lanePadding = 5;
    private const int laneSpacing = 20;
    private const int laneMultiplier = 2;
    private const int firstRowPos = 0;

    public Environment(ScreenBuffer screenBuffer)
    {
        this.drawEnvironment(screenBuffer);
    }

    //get targeted lane positions
    public static int[] getLanePositions()
    {
        int middleLanePosition = Screen.screenWidth / 2;
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
            for (int j = 0; j < Screen.screenHeight; j++)
            {
                int rowPosition = j;
                int columnPosition = lanePositons[i] - lanePadding;

                screenBuffer.writeChar(Text.createBufferChar(roadBorder, rowPosition, columnPosition));
            }
        }
    }

    private void writeGameInstructions(ScreenBuffer screenBuffer)
    {
        string instructions = "Use A and D to steer car left or right.";
        screenBuffer.writeLine(Text.createLeftAlignedBufferString(instructions, firstRowPos));
    }
}

