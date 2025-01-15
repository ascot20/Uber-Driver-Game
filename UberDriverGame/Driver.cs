using System.Text.Json.Serialization;
using Utilities;

class Driver
{
    private const int minLane = 1;
    private const int maxLane = 3;
    private const int defaultStartingLane = 2;
    private const string car =
            "  .#████#.\r\n" +
            " |████████|\r\n" +
            " |████████|\r\n" +
            "_|        |_\r\n" +
            " |________|\r\n" +
            " |##    ##|\r\n" +
            " |████████|\r\n" +
            " |████████|\r\n" +
            " |##████##|\r\n" +
            " |#______#|\r\n" +
            " .#.    .#.\r\n" +
            "  :#████#:";

    [JsonInclude] public string username;
    [JsonInclude] public decimal totalEarnings;
    [JsonInclude] public int currentLane;
    public int carHeight = Text.getHeightOfString(car);
    private BufferString carBuffer;

    public Driver()
    {
    }

    public Driver(string username, int startingLane)
    {
        this.username = username;
        this.totalEarnings = 0;

        if (startingLane < minLane || startingLane > maxLane) 
        {
            this.currentLane = defaultStartingLane;
        }
        else
        {
            this.currentLane = startingLane;
        }
        
    }

    public void deployCar(ScreenBuffer screenBuffer)
    {
        this.updateCarPosition(screenBuffer);
    }

    public void steerLeft(ScreenBuffer screenBuffer)
    {
        if (this.currentLane > minLane)
        {
            screenBuffer.clearLines(this.carBuffer);
            this.currentLane -= 1;
            this.updateCarPosition(screenBuffer);
        }
    }

    public void steerRight(ScreenBuffer screenBuffer)
    {
        if (this.currentLane < maxLane)
        {
            screenBuffer.clearLines(this.carBuffer);
            this.currentLane += 1;
            this.updateCarPosition(screenBuffer);
        }
    }

    private void updateCarPosition(ScreenBuffer screenBuffer)
    {
        this.carBuffer = Text.createBottomCenteredBufferString(car);

        this.carBuffer.xPos = Environment.getLanePositions()[this.currentLane - 1];

        screenBuffer.writeLines(this.carBuffer);
    }
}

