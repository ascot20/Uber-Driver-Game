class ObstacleManager
{
    private string carObstacle1 =
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
    private string carObstacle2 = "";
    private string carObstacle3 = "";
    Obstacle dummy;

    public ObstacleManager()
    {
        this.dummy = new Obstacle(carObstacle1);
        
    }

    public void moveObstacles(ScreenBuffer screenBuffer)
    {
        this.dummy.moveObstacle(screenBuffer);
    }

    //private void clearObstacles(ScreenBuffer screenBuffer)
    //{
    //    this.dummy.clearObstacle(screenBuffer);
    //}
}
