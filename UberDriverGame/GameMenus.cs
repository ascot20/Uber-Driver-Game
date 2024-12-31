using System;
using System.Collections.Generic;

class GameMenus
{

    private static void drawASCIIArt()
    {
        string gameTitleInASCII = "██╗   ██╗██████╗ ███████╗██████╗     ██████╗ ██████╗ ██╗██╗   ██╗███████╗██████╗ \r\n" +
                                    "██║   ██║██╔══██╗██╔════╝██╔══██╗    ██╔══██╗██╔══██╗██║██║   ██║██╔════╝██╔══██╗\r\n" +
                                    "██║   ██║██████╔╝█████╗  ██████╔╝    ██║  ██║██████╔╝██║██║   ██║█████╗  ██████╔╝\r\n" +
                                    "██║   ██║██╔══██╗██╔══╝  ██╔══██╗    ██║  ██║██╔══██╗██║╚██╗ ██╔╝██╔══╝  ██╔══██╗\r\n" +
                                    "╚██████╔╝██████╔╝███████╗██║  ██║    ██████╔╝██║  ██║██║ ╚████╔╝ ███████╗██║  ██║\r\n" +
                                    " ╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝\r\n";

        Utilities.horCenterMultiLineString(gameTitleInASCII);
    }


    public static int displayStartMenu()
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
            drawASCIIArt();

            Utilities.horCenterMultiLineString("Use Up or Down arrow key to select option.\n");
            for (int i = 0; i < menuOptionLength; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Utilities.horCenterMultiLineString(menuOption[i] + " <--");
                }
                else
                {
                    Utilities.horCenterMultiLineString(menuOption[i]);
                }

                Console.WriteLine();
            }

            key = Console.ReadKey(true).Key;
            Utilities.checkConsoleSize();

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

    public static string displayNewGameMenu()
    {
        Console.CursorVisible = true;
        string username;
        do
        {
            drawASCIIArt();

            Utilities.horCenterString("Enter driver name: ");
            
            username = Console.ReadLine();
            Utilities.checkConsoleSize();
        } while (username.Length == 0 || username == "");

        Console.CursorVisible = false;

        return username;
    }

}

