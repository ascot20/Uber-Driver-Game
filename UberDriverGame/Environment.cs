using System;

class Environment
{
    private int[] roadLanesoffsets;
    private char roadStrip = '║';
    private int lanePadding = 5;

    public Environment(int[] lanesOffsets)
    {
        this.roadLanesoffsets = lanesOffsets;
        drawRoad();
    }

    private void drawRoad()
    {
        for (int i = 0; i < roadLanesoffsets.Length; i++)
        {
            int bottomOffset = -1;
            int windowHeight = Console.WindowHeight + bottomOffset;
            for (int j = 0; j < windowHeight; j++)
            {
                Console.SetCursorPosition(roadLanesoffsets[i] - lanePadding, j);
                Console.WriteLine(roadStrip);
            }
        }
    }
}

