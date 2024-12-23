using System;
using System.Collections.Generic;

class GameMenu
{

    private static void drawASCIIArt()
    {
        string gameTitleInASCII = ".---------------------------------------------------------------.\r\n" +
                                    "| _   _ ____  _____ ____    ____  ____  _____     _______ ____  |\r\n" +
                                    "|| | | | __ )| ____|  _ \\  |  _ \\|  _ \\|_ _\\ \\   / / ____|  _ \\ |\r\n" +
                                    "|| | | |  _ \\|  _| | |_) | | | | | |_) || | \\ \\ / /|  _| | |_) ||\r\n" +
                                    "|| |_| | |_) | |___|  _ <  | |_| |  _ < | |  \\ V / | |___|  _ < |\r\n|" +
                                    " \\___/|____/|_____|_| \\_\\ |____/|_| \\_\\___|  \\_/  |_____|_| \\_\\|\r\n" +
                                    "'---------------------------------------------------------------'";

        Utilities.centerString(gameTitleInASCII);
    }

    public static int display()
    {
        drawASCIIArt();

        List<string> menuOption = new List<string>();
        menuOption.Add("New Game");
        menuOption.Add("Load Game");
        menuOption.Add("Exit");

        Console.WriteLine();
        for (int i = 0; i < menuOption.Count; i++)
        {
            Utilities.centerString(menuOption[i]);
            Console.WriteLine();
        }

        return 0;
    }
}

