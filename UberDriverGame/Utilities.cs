using System;

class Utilities
{
    public static void centerString(string text)
    {
        string[] lines = text.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineLength = line.Length;

            Console.SetCursorPosition((Console.WindowWidth - lineLength) / 2, Console.CursorTop);
            Console.WriteLine(line);
        }
    }
}
