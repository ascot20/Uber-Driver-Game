using System;

class Environment
{
    private int[] roadLaneoffsets;
    private char roadStrip = '║';
    private int lanePadding = 5;

    public Environment(int[] laneOffsets)
    {
        this.roadLaneoffsets = laneOffsets;
        drawRoad();
    }

    private void drawRoad()
    {
        for (int i = 0; i < roadLaneoffsets.Length; i++)
        {
            int bottomOffset = -1;
            int windowHeight = Console.WindowHeight + bottomOffset;
            for (int j = 0; j < windowHeight; j++)
            {
                Console.SetCursorPosition(roadLaneoffsets[i] - lanePadding, j);
                Console.WriteLine(roadStrip);
            }
        }
    }
}

