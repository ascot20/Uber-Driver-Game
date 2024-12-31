using System;

class Utilities
{
    //constants
    const int minWindowWidth = 94;
    const int minWindowHeight = 37;
    const string clearScreenEscSeq = "\x1b[3J";
    const string separator = "\n";


    public static void horizontalCenterCursor(string text)
    {
        int textLength = text.Length;

        Console.SetCursorPosition((Console.WindowWidth - textLength) / 2, Console.CursorTop);
    }


    public static void verticalCenterCursor(string text)
    {
        int numOfLines = text.Split(separator).Length;

        Console.SetCursorPosition(Console.CursorLeft, (Console.WindowHeight - numOfLines) / 2);
    }


    public static void bottomCenterCursor(string text)
    {
        int numOfLines = text.Split(separator).Length;
        int textLength = text.Length;

        Console.SetCursorPosition((Console.WindowWidth - textLength) / 2, Console.WindowHeight - numOfLines);
    }


    public static void horCenterString(string text)
    {
        horizontalCenterCursor(text);
        Console.Write(text);
    }

    public static void horCenterMultiLineString(string text)
    {
        string[] lines = text.Split(separator);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            horizontalCenterCursor(line);
            Console.WriteLine(line);
        }
    }

    public static void bottomCenterMultiLineString(string text)
    {
        string[] lines = text.Split(separator);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            bottomCenterCursor(line);
            Console.WriteLine(line);
        }
    }

    public static void checkConsoleSize()
    {
        bool sizeCheckPass;

        do
        {
            //clear entire console including text not in view
            Console.Clear();
            Console.WriteLine(clearScreenEscSeq);

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            if (windowWidth < minWindowWidth || windowHeight < minWindowHeight)
            {
                sizeCheckPass = false;
                horCenterMultiLineString("Please resize window and press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                sizeCheckPass = true;
            }

        } while (!sizeCheckPass);
    }
}
