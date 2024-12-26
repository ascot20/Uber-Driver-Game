using System;

class Utilities
{
    //constants
    const int minWindowWidth = 84;
    const int minWindowHeight = 27;
    const string clearScreenEscSeq = "\x1b[3J";


    public static void centerString(string text)
    {
        int textLength = text.Length;

        Console.SetCursorPosition((Console.WindowWidth - textLength) / 2, Console.CursorTop);
        Console.Write(text);
    }


    public static void centerStrings(string text)
    {
        string[] lines = text.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            centerString(line);
            Console.WriteLine();
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
                centerStrings("Please resize window and press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                sizeCheckPass = true;
            }

        } while (!sizeCheckPass);
    }
}
