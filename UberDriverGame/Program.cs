using System;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        while (true)
        {
            Utilities.checkConsoleSize();
            GameMenu.display();
        }
    }
}

