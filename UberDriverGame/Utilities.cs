using System;

class Utilities
{
    //constants
    const int minWindowWidth = 170;
    const int minWindowHeight = 40;
    const string clearScreenEscSeq = "\x1b[3J";
    const string separator = "\n";
    public static int screenWidth;
    public static int screenHeight;


    public static void horizontalCenterCursor(int textWidth)
    {
        Console.SetCursorPosition((Console.WindowWidth - textWidth) / 2, Console.CursorTop);
    }

    public static BufferString createBottomCenteredBufferString(string text)
    {
        int textWidth = text.Length;
        int textHeight = text.Split(separator).Length;

        BufferString s;
        s.xPos = Console.WindowWidth - textWidth / 2;
        s.yPos = Console.WindowHeight - textHeight;
        s.text = text;

        return s;
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
        int currentWindowWidth;
        int currentWindowHeight;

        do
        {
            //clear entire console including text not in view
            Console.Clear();
            Console.WriteLine(clearScreenEscSeq);

            currentWindowWidth = Console.WindowWidth;
            currentWindowHeight = Console.WindowHeight;

            if (currentWindowWidth < minWindowWidth || currentWindowHeight < minWindowHeight)
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

        screenWidth = currentWindowWidth;
        screenHeight = currentWindowHeight;
    }
}

