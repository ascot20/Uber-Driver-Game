using System;
using System.Collections.Generic;
using Utilities;

class GameMenus
{
    private const string selector = " <--";
    private const int thirdRow = 2;
    private const int fourthRow = 3;
    private const string gameTitleInASCII = 
                                    "██╗   ██╗██████╗ ███████╗██████╗     ██████╗ ██████╗ ██╗██╗   ██╗███████╗██████╗ \r\n" +
                                    "██║   ██║██╔══██╗██╔════╝██╔══██╗    ██╔══██╗██╔══██╗██║██║   ██║██╔════╝██╔══██╗\r\n" +
                                    "██║   ██║██████╔╝█████╗  ██████╔╝    ██║  ██║██████╔╝██║██║   ██║█████╗  ██████╔╝\r\n" +
                                    "██║   ██║██╔══██╗██╔══╝  ██╔══██╗    ██║  ██║██╔══██╗██║╚██╗ ██╔╝██╔══╝  ██╔══██╗\r\n" +
                                    "╚██████╔╝██████╔╝███████╗██║  ██║    ██████╔╝██║  ██║██║ ╚████╔╝ ███████╗██║  ██║\r\n" +
                                    " ╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝\r\n";

    private static void drawGameTitle()
    {
        Text.alignCenter(gameTitleInASCII);
    }


    public static int displayStartMenu(int minWindowWidth, int minWindowHeight)
    {
        List<string> menuOptions = new List<string> { "New Game", "Load Game", "Exit" };

        int menuOptionLength = menuOptions.Count;
        int selectedOptionIndex = 0;
        ConsoleKey key;

        do
        {
            drawGameTitle();

            Text.alignCenter("Use Up or Down arrow key to select option.\n");
            for (int i = 0; i < menuOptionLength; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Text.alignCenter(menuOptions[i] + selector);
                }
                else
                {
                    Text.alignCenter(menuOptions[i]);
                }

                Console.WriteLine();
            }

            key = Console.ReadKey(true).Key;
            Screen.setupConsoleSize(minWindowWidth, minWindowHeight);

            if (key == ConsoleKey.UpArrow)
            {
                selectedOptionIndex = (selectedOptionIndex - 1 + menuOptionLength) % menuOptionLength;
            }

            else if (key == ConsoleKey.DownArrow)
            {
                selectedOptionIndex = (selectedOptionIndex + 1) % menuOptionLength;
            }

            Screen.clearConsole();
        } while (key != ConsoleKey.Enter);

        return selectedOptionIndex;
    }

    public static string displayNewGameMenu(int minWindowWidth, int minWindowHeight)
    {
        Console.CursorVisible = true;

        string username;

        do
        {
            drawGameTitle();

            Text.alignCenter("Enter driver name: ");

            username = Console.ReadLine();

            Screen.setupConsoleSize(minWindowWidth, minWindowHeight);
            Screen.clearConsole();

        } while (string.IsNullOrWhiteSpace(username));

        Console.CursorVisible = false;

        return username;
    }

    public static bool displayCollisionMenu(ScreenBuffer screenBuffer)
    {
        BufferString crushMessage = Text.createLeftAlignedBufferString("You crushed.", thirdRow);
        BufferString continueMessage = Text.createLeftAlignedBufferString("Ride again(Y/N)?", fourthRow);

        screenBuffer.writeLine(crushMessage);
        screenBuffer.writeLine(continueMessage);

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

