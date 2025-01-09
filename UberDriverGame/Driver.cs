class Driver
{
    //constants
    const int defaultLane = 2;
    const int minLane = 1;
    const int maxLane = 3;
    const int bottomOffset = -1;

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

    //fields
    private string username;
    public decimal totalEarnings;
    public int carHeight = Utilities.getHeightOfString(car);
    public int currentLane;
    private BufferString carBuffer;

    public Driver(string username, ScreenBuffer screenBuffer)
    {
        this.username = username;
        this.totalEarnings = 0;
        this.currentLane = defaultLane;
        this.deployCar(screenBuffer);
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
        this.carBuffer = Utilities.createBottomCenteredBufferString(car);

        this.carBuffer.xPos = Environment.getLanePositions()[this.currentLane - 1];
        this.carBuffer.yPos = this.carBuffer.yPos + bottomOffset;

        screenBuffer.writeLines(this.carBuffer);
    }
}

