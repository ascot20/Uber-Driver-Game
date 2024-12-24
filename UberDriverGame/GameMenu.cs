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
        List<string> menuOption = new List<string>();
        menuOption.Add("New Game");
        menuOption.Add("Load Game");
        menuOption.Add("Exit");

        int menuOptionLength = menuOption.Count;
        int selectedOptionIndex = 0;
        ConsoleKey key;
        do
        {
            Console.Clear();
            drawASCIIArt();

            Utilities.centerString("Use Up or Down arrow key to select option.\n");
            for (int i = 0; i < menuOptionLength; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Utilities.centerString(menuOption[i] + " <--");
                }
                else
                {
                    Utilities.centerString(menuOption[i]);
                }

                Console.WriteLine();
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedOptionIndex = (selectedOptionIndex - 1 + menuOptionLength) % menuOptionLength;
            }

            else if (key == ConsoleKey.DownArrow)
            {
                selectedOptionIndex = (selectedOptionIndex + 1) % menuOptionLength;
            }

        } while (key != ConsoleKey.Enter);


        return selectedOptionIndex;
    }
}

