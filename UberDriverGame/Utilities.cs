using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Utilities
{
    class Screen
    {
        private const string clearScreenEscSeq = "\x1b[3J";

        public static int screenWidth;
        public static int screenHeight;

        public static void setupConsoleSize(int minWindowWidth, int minWindowHeight)
        {
            int currentWindowWidth = Console.WindowWidth;
            int currentWindowHeight = Console.WindowHeight;

            //Execute this loop if the currentWindowWidth or currentWindowHeight is less than
            //the minimum required dimensions
            while (currentWindowWidth < minWindowWidth || currentWindowHeight < minWindowHeight)
            {
                clearConsole();

                Text.alignCenter("Please maximize window and press any key to continue.");
                Console.ReadKey(true);

                currentWindowWidth = Console.WindowWidth;
                currentWindowHeight = Console.WindowHeight;

                clearConsole();
            }

            screenWidth = currentWindowWidth;
            screenHeight = currentWindowHeight;
        }


        public static void clearConsole()
        {
            //clear entire console including text not in current view
            Console.Clear();
            Console.WriteLine(clearScreenEscSeq);
        }
    }

    class Text
    {
        private const string separator = "\n";
        const int firstColumnPos = 0;
        const int firstRowPos = 0;

        public static BufferChar createBufferChar(char character, int rowPosition, int columnPosition)
        {
            if (rowPosition > Screen.screenHeight)
            {
                throw new Exception($"rowPosition must be less than {Screen.screenHeight}.");
            }

            if (columnPosition > Screen.screenWidth)
            {
                throw new Exception($"columnPosition must be less than {Screen.screenWidth}");
            }

            return new BufferChar { character = character, xPos = columnPosition, yPos = rowPosition };
        }

        public static BufferString createBufferString(string text, int rowPosition, int columnPosition)
        {
            if (rowPosition > Screen.screenHeight)
            {
                throw new Exception($"rowPosition must be less than {Screen.screenHeight}.");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("text must not be empty.");
            }

            if (columnPosition > Screen.screenWidth)
            {
                throw new Exception($"columnPosition must be less than {Screen.screenWidth}");
            }

            return new BufferString { text = text, xPos = columnPosition, yPos = rowPosition };
        }

        public static BufferString createBottomCenteredBufferString(string text)
        {
            int textWidth = text.Length;
            int textHeight = getHeightOfString(text);
            int columnPosition = Screen.screenWidth - textWidth / 2;
            int rowPosition = Screen.screenHeight - textHeight;

            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("text must not be empty.");
            }

            if (textWidth > Screen.screenWidth)
            {
                columnPosition = firstColumnPos;
            }

            if (textHeight > Screen.screenHeight)
            {
                rowPosition = firstRowPos;
            }

            return new BufferString { text = text, xPos = columnPosition, yPos = rowPosition };
        }

        public static BufferString createRightAlignedBufferString(string text, int rowPosition)
        {
            if (rowPosition > Screen.screenHeight)
            {
                throw new Exception($"rowPosition must be less than {Screen.screenHeight}.");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("text must not be empty.");
            }

            int textWidth = text.Length;
            int columnPosition = Screen.screenWidth - textWidth;

            return new BufferString { text = text, xPos = columnPosition, yPos = rowPosition };
        }

        public static BufferString createLeftAlignedBufferString(string text, int rowPosition)
        {
            if (rowPosition > Screen.screenHeight)
            {
                throw new Exception($"rowPosition must be less than {Screen.screenHeight}.");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("text must not be empty.");
            }

            return new BufferString { text = text, xPos = firstColumnPos, yPos = rowPosition };
        }

        public static void alignCenter(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("text must not be empty.");
            }

            string[] lines = text.Split(separator);

            if (lines.Length == 1)
            {
                string line = lines[0];
                centerCursor(line.Length);
                Console.Write(line);
            }

            else
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    centerCursor(line.Length);
                    Console.WriteLine(line);
                }
            }
        }

        public static void centerCursor(int textWidth)
        {
            if (textWidth > Console.WindowWidth)
            {
                Console.SetCursorPosition(firstColumnPos, Console.CursorTop);
            }
            else
            {
                Console.SetCursorPosition((Console.WindowWidth - textWidth) / 2, Console.CursorTop);
            }

        }

        public static int getHeightOfString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("text must not be empty.");
            }

            return text.Split(separator).Length;
        }

        public static void clearBufferStrings(List<BufferString> bufferStrings, ScreenBuffer screenBuffer)
        {
            for (int i = 0; i < bufferStrings.Count; i++)
            {
                screenBuffer.clearLine(bufferStrings[i]);
            }
            bufferStrings.Clear();
        }
    }

    class FileHelper
    {
        public static void SaveData<T>(T data, string filePath)
        {
            StreamWriter outputFile = new StreamWriter(filePath);

            string jsonText = JsonSerializer.Serialize(data);

            outputFile.Write(jsonText);

            outputFile.Close();
        }

        public static T LoadData<T>(string filePath)
        {
            try
            {
                StreamReader inputFile = new StreamReader(filePath);
                string jsonText = inputFile.ReadLine();
                inputFile.Close();

                T data = JsonSerializer.Deserialize<T>(jsonText);

                return data;
            }

            catch
            {
                throw new Exception("No data");
            }
          
        }
    }
}