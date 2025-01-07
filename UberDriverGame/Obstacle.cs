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
    private string[] carObstacleParts;
    public int carObstacleHeight;
    public int currentLane;
    public int firstRowPosition;
    private List<BufferString> carObstacleBuffers;

    static Random generator = new Random();

    public Obstacle(string carObstacle, int firstRowPosition)
    {
        this.carObstacle = carObstacle;
        this.carObstacleParts = this.carObstacle.Split(separator);
        this.carObstacleHeight = this.carObstacleParts.Length;
        this.currentLane = generator.Next(minLane, maxLane + 1);
        this.firstRowPosition = firstRowPosition;
        this.carObstacleBuffers = new List<BufferString>();
    }

    //write obstacle to screen buffer and add to list
    public void moveObstacle(ScreenBuffer screenBuffer)
    {
        this.clearObstacle(screenBuffer);

        BufferString carObstacleBuffer;

        for (int i = 0; i < this.carObstacleHeight; i++)
        {
            int currentRow = this.firstRowPosition + i;

            if (currentRow >= 0 && currentRow < Utilities.screenHeight)
            {
                string carPart = this.carObstacleParts[i];
                carObstacleBuffer.xPos = Environment.getLanePositions()[currentLane - 1];
                carObstacleBuffer.yPos = currentRow;
                carObstacleBuffer.text = carPart;

                screenBuffer.writeLine(carObstacleBuffer);
                this.carObstacleBuffers.Add(carObstacleBuffer);
            }
        }
        this.firstRowPosition += 1;
    }

    //clear buffers in buffer before every move
    private void clearObstacle(ScreenBuffer screenBuffer)
    {
        for (int i = 0; i < this.carObstacleBuffers.Count; i++)
        {
            screenBuffer.clearLine(this.carObstacleBuffers[i]);
        }
        this.carObstacleBuffers.Clear();
    }
}

