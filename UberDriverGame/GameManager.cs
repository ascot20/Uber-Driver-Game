using System;

class GameManager
{
    public GameManager()
    {
        Console.CursorVisible = false;
        Utilities.checkConsoleSize();

        int optionSelected = GameMenus.displayStartMenu();

        switch (optionSelected)
        {
            case 0:
                Console.WriteLine("Nothing");
                break;
        }
    }
}

