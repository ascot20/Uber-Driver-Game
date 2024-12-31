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
    private int[] laneOffsets = { 60,80,100 };

    public Driver(string username)
    {
        this.username = username;
        deployCar();
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
        Console.Clear();

        int carHeight = car.Split("\n").Length;
        Utilities.bottomCenterCursor(carHeight);


        string[] carParts = car.Split("\n");
        for (int i = 0; i < carHeight; i++)
        {
            string carPart = carParts[i];
            Console.SetCursorPosition(laneOffsets[currentLane - 1], Console.CursorTop);
            Console.WriteLine(carPart);
        }
    }
}
