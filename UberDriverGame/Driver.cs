using System;

class Driver
{
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
    private int currentLane = 2;
    private int laneSpacing = 20;
    private int[] laneOffsets;

    public Driver(string username)
    {
        this.username = username;
        initializeLaneOffsets();
        deployCar();
    }

    private void initializeLaneOffsets()
    {
        int middleLaneOffset = Console.WindowWidth / 2;
        laneOffsets = new int[] { middleLaneOffset - laneSpacing, middleLaneOffset, middleLaneOffset + laneSpacing,
            middleLaneOffset + laneSpacing * 2 };
    }

    private void deployCar()
    {
        updateCarPosition();
    }

    public void steerLeft()
    {
        if (currentLane > 1)
        {
            currentLane -= 1;
            updateCarPosition();
        }

    }

    public void steerRight()
    {
        if (currentLane < 3)
        {
            currentLane += 1;
            updateCarPosition();
        }
    }

    private void updateCarPosition()
    {
        Utilities.checkConsoleSize();

        Environment driverEnvironment = new Environment(laneOffsets);

        int carHeight = car.Split("\n").Length;
        int bottomOffset = 3;

        Utilities.bottomCenterCursor(carHeight, bottomOffset);


        string[] carParts = car.Split("\n");
        for (int i = 0; i < carHeight; i++)
        {
            string carPart = carParts[i];
            Console.SetCursorPosition(laneOffsets[currentLane - 1], Console.CursorTop);
            Console.WriteLine(carPart);
        }
    }
}
