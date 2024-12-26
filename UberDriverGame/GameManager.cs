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
                GameMenus.displayNewGameMenu();
                break;
            case 1:
                Console.WriteLine("Load game");
                break;
            case 2:
                Console.WriteLine("Exit game");
                break;
        }
    }
}

