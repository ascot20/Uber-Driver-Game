using System.Collections.Generic;

class ObstacleManager
{
    //constants
    const string separator = "\r\n";
    private const string carObstacle =
        " .#████████#.\r\n" +
        " .██████████.\r\n" +
        " .#       ##.\r\n" +
        ".:#####%@@%#:.\r\n" +
        " .  ██████  .\r\n" +
        " .%#██████#%.\r\n" +
        " .  ██████  .\r\n" +
        " .##########.\r\n" +
        " .#        #.\r\n" +
        " .%%      %%.\r\n" +
        " .%%      %%.\r\n" +
        " .#        #.\r\n" +
        " .=+------+=.\r\n";
    private const int spacingMultiplier = 2;

    //fields
    public List<Obstacle> obstacles;
    private int firstObstacleStartingRow;

    public ObstacleManager()
    {
        this.firstObstacleStartingRow = -carObstacle.Split(separator).Length;
        this.obstacles = new List<Obstacle>();

    }

    //add obstacle to list
    public void addObstacle()
    {
        if (this.obstacles.Count == 0)
        {
            this.obstacles.Add(new Obstacle(carObstacle, this.firstObstacleStartingRow));
        }

        else
        {
            Obstacle previousObstacle = this.obstacles[this.obstacles.Count - 1];
            int previousObstacleRow = previousObstacle.firstRowPosition;
            int ObstacleStartingRowPos = previousObstacle.firstRowPosition - (previousObstacle.carObstacleHeight * spacingMultiplier);

            this.obstacles.Add(new Obstacle(carObstacle, ObstacleStartingRowPos));
        }
    }

    //move obstacles
    public void moveObstacles(ScreenBuffer screenBuffer)
    {
        for (int i = 0; i < this.obstacles.Count; i++)
        {
            //if obstacle has moved out of view then remove from list
            if (this.obstacles[i].firstRowPosition > Utilities.screenHeight)
            {
                this.obstacles.RemoveAt(i);
            }
            else
            {
                this.obstacles[i].moveObstacle(screenBuffer);
            }
        }
    }

}
