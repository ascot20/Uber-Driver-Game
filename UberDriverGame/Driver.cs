class Driver
{
    //constants
    const int defaultLane = 2;
    const int minLane = 1;
    const int maxLane = 3;

    //fields
    private string username;
    private string car =
            "  .#████#.\r\n" +
            " .████████.\r\n" +
            " :████████:\r\n" +
            ".:*      *:.\r\n" +
            " .-+*##*+-.\r\n" +
            " .=%    %=.\r\n" +
            " .████████.\r\n" +
            " .████████.\r\n" +
            " .=%████%=:\r\n" +
            " .%......%:\r\n" +
            " .#.    .#.\r\n" +
            "  :%████%:\r\n";
    private int currentLane;
    private BufferString carBuffer;

    public Driver(string username, ScreenBuffer screenBuffer)
    {
        this.username = username;
        this.currentLane = defaultLane;
        this.deployCar(screenBuffer);
    }

    private void deployCar(ScreenBuffer screenBuffer)
    {
        this.updateCarPosition(screenBuffer);
    }

    public void steerLeft(ScreenBuffer screenBuffer)
    {
        if (this.currentLane > minLane)
        {
            screenBuffer.clearLines(carBuffer);
            this.currentLane -= 1;
            this.updateCarPosition(screenBuffer);
        }

    }

    public void steerRight(ScreenBuffer screenBuffer)
    {
        if (this.currentLane < maxLane)
        {
            screenBuffer.clearLines(carBuffer);
            this.currentLane += 1;
            this.updateCarPosition(screenBuffer);
        }
    }

    private void updateCarPosition(ScreenBuffer screenBuffer)
    {
        this.carBuffer = Utilities.createBottomCenteredBufferString(this.car);

        this.carBuffer.xPos = Environment.laneOffsets[this.currentLane - 1];
        screenBuffer.writeLines(this.carBuffer);
    }
}

