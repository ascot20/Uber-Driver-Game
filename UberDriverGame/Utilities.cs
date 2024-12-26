using System;

class Utilities
{
    //constants
    const int minWindowWidth = 84;
    const int minWindowHeight = 27;
    const string clearScreenEscSeq = "\x1b[3J";

    public static void centerString(string text)
    {
        string[] lines = text.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineLength = line.Length;
            int cursorLeftPosition = (Console.WindowWidth - lineLength) / 2;

            Console.SetCursorPosition(cursorLeftPosition, Console.CursorTop);
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
                centerString("Please resize window and press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                sizeCheckPass = true;
            }

        } while (!sizeCheckPass);
    }
}
