using System.Collections.Generic;
using Utilities;

class ObstacleManager
{
    //constants
    private const string carObstacle =
        " .#████████#.\r\n" +
        " |██████████|\r\n" +
        " |          |\r\n" +
        "_|__________|_\r\n" +
        " |  ██████  |\r\n" +
        " |__██████__|\r\n" +
        " |  ██████  |\r\n" +
        " |##______##|\r\n" +
        " |█        █|\r\n" +
        " |█        █|\r\n" +
        " |█        █|\r\n" +
        " |█________█|\r\n" +
        " :#________#:";
    private const int spacingMultiplier = 2;

    //fields
    private List<Obstacle> obstacles;
    private int firstObstacleStartingRow;

    public ObstacleManager()
    {
        this.firstObstacleStartingRow = -Text.getHeightOfString(carObstacle);
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
    public void moveObstacles(Driver driver, ScreenBuffer screenBuffer, AccountManager accountManager)
    {
        for (int i = this.obstacles.Count - 1; i >= 0; i--)
        {
            //if obstacle has moved out of view then remove from list
            if (this.obstacles[i].firstRowPosition > Screen.screenHeight)
            {
                this.obstacles.RemoveAt(i);
                accountManager.addEarnings(driver, screenBuffer);
            }
            else
            {
                this.obstacles[i].moveObstacle(screenBuffer);
            }
        }
    }

    public bool checkForCollision(Driver driver)
    {
        for (int i = 0; i < this.obstacles.Count; i++)
        {
            Obstacle obstacle = this.obstacles[i];

            // if obstacle is on the same lane and row positions
            if (
                (obstacle.currentLane == driver.currentLane) && 
                (obstacle.firstRowPosition + obstacle.carObstacleHeight > Screen.screenHeight - driver.carHeight))
            {
                return true;
            }
        }
        return false;
    }

    public void clearObstacles()
    {
        this.obstacles.Clear();
    }

}
