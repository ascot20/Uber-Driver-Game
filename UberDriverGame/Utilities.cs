using System;

class Utilities
{
    //constants
    const int minWindowWidth = 170;
    const int minWindowHeight = 40;
    const string clearScreenEscSeq = "\x1b[3J";
    const string separator = "\n";


    public static void horizontalCenterCursor(int textWidth)
    {
        Console.SetCursorPosition((Console.WindowWidth - textWidth) / 2, Console.CursorTop);
    }


    public static void verticalCenterCursor(int textHeight)
    {
        Console.SetCursorPosition(Console.CursorLeft, (Console.WindowHeight - textHeight) / 2);
    }


    public static void bottomCenterCursor(int textHeight, int offset)
    {
        int bottomOffset = -offset;

        Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight - textHeight) + bottomOffset);
    }


    public static void horCenterString(string text)
    {
        horizontalCenterCursor(text.Length);
        Console.Write(text);
    }

    public static void horCenterMultiLineString(string text)
    {
        string[] lines = text.Split(separator);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            horizontalCenterCursor(line.Length);
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
                horCenterMultiLineString("Please maximize window and press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                sizeCheckPass = true;
            }

        } while (!sizeCheckPass);
    }
}
