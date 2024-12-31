using System;

class Driver
{
    private string username;
    private string car =
            "  .#████#.\r\n" +
            " .████████.\n" +
            " :████████:\n" +
            ".:*      *:.\n" +
            " .-+*##*+-.\n" +
            " .=%    %=.\n" +
            " .████████.\n" +
            " .████████.\n" +
            " .=%████%=:\n" +
            " .%......%:\n" +
            " .#.    .#.\n" +
            "  :%████%: ";

    public Driver(string username)
    {
        this.username = username;
        deployCar();
    }

    private void deployCar()
    {
        Utilities.bottomCenterMultiLineString(car);
        Console.ReadKey();
    }
}
