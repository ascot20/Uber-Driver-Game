using System;
using System.Collections.Generic;

class GameMenus
{
    //constants
    const string selector = " <--";
    const int thirdRow = 2;
    const int fourthRow = 3;

    private static void drawGameTitle()
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
        List<string> menuOption = new List<string> { "New Game", "Load Game", "Exit" };

        int menuOptionLength = menuOption.Count;
        int selectedOptionIndex = 0;
        ConsoleKey key;

        do
        {
            drawGameTitle();

            Utilities.horCenterMultiLineString("Use Up or Down arrow key to select option.\n");
            for (int i = 0; i < menuOptionLength; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Utilities.horCenterMultiLineString(menuOption[i] + selector);
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
            drawGameTitle();

            Utilities.horCenterString("Enter driver name: ");

            username = Console.ReadLine();
            Utilities.checkConsoleSize();

        } while (string.IsNullOrWhiteSpace(username));

        Console.CursorVisible = false;

        return username;
    }

    public static bool displayCollisionMenu(ScreenBuffer screenBuffer)
    {
        BufferString crushMessage = Utilities.createLeftAlignedBufferString("You crushed. £10 was used for repairs.", thirdRow);
        BufferString continueMessage = Utilities.createLeftAlignedBufferString("Ride again(Y/N)?", fourthRow);

        screenBuffer.writeLines(crushMessage);
        screenBuffer.writeLines(continueMessage);
        screenBuffer.renderToConsole();

        ConsoleKey key;

        do
        {
            key = Console.ReadKey(true).Key;
        } while (key != ConsoleKey.Y && key != ConsoleKey.N);

        if (key == ConsoleKey.Y)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}

