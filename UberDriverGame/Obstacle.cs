using System;
using System.Collections.Generic;

class Obstacle
{
    //constants
    private const string separator = "\r\n";
    private const int minLane = 1;
    private const int maxLane = 3;

    //fields
    private string carObstacle;
    private int currentLane;
    private int firstRowPosition;
    private List<BufferString> carObstacleBuffers;

    public Obstacle(string carObstacle)
    {
        Random generator = new Random();
        this.carObstacle = carObstacle;
        this.currentLane = generator.Next(minLane, maxLane + 1);
        this.firstRowPosition = -carObstacle.Split(separator).Length;
        this.carObstacleBuffers = new List<BufferString>();
    }

    //write obstacle to screen buffer and add to list
    public void moveObstacle(ScreenBuffer screenBuffer)
    {
        this.clearObstacle(screenBuffer);

        BufferString carObstacleBuffer;
        int carHeight = carObstacle.Split(separator).Length;
        string[] carParts = carObstacle.Split(separator);

        for (int i = 0; i < carHeight; i++)
        {
            int currentRow = firstRowPosition + i;

            if (currentRow >= 0 && currentRow < Console.WindowHeight)
            {
                string carPart = carParts[i];
                carObstacleBuffer.xPos = Environment.laneOffsets[currentLane - 1];
                carObstacleBuffer.yPos = currentRow;
                carObstacleBuffer.text = carPart;
                screenBuffer.writeLine(carObstacleBuffer);
                carObstacleBuffers.Add(carObstacleBuffer);
            }
        }
        firstRowPosition += 1;
    }

    //clear buffers in before every move
    private void clearObstacle(ScreenBuffer screenBuffer)
    {
        for (int i = 0; i < carObstacleBuffers.Count; i++)
        {
            screenBuffer.clearLine(carObstacleBuffers[i]);
        }
        carObstacleBuffers.Clear();
    }
}

