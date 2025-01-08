using System;
using System.Threading;

class GameManager
{
    //constants
    const int collisionOffset = -1;

    //fields
    int timeout = 50;

    public GameManager()
    {

        Console.CursorVisible = false;

        Utilities.checkConsoleSize();

        int optionSelected = GameMenus.displayStartMenu();

        switch ((Option)optionSelected)
        {
            case Option.NewGame:
                string username = GameMenus.displayNewGameMenu();
                ScreenBuffer screenBuffer = new ScreenBuffer(Utilities.screenWidth, Utilities.screenHeight);
                Environment environment = new Environment(screenBuffer);
                Driver driver = new Driver(username, screenBuffer);
                handleInGameInput(driver, screenBuffer);
                break;
            case Option.LoadGame:
                Console.WriteLine("Load game");
                break;
            case Option.Exit:
                Console.WriteLine("Exit game");
                break;
        }
    }


    private void handleInGameInput(Driver driver, ScreenBuffer screenBuffer)
    {

        ObstacleManager obstacleManager = new ObstacleManager();

        bool isRunning = true;

        while (isRunning)
        {
            obstacleManager.addObstacle();
            obstacleManager.moveObstacles(screenBuffer);

            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        driver.steerLeft(screenBuffer);
                        break;

                    case ConsoleKey.D:
                        driver.steerRight(screenBuffer);
                        break;

                }
            }

            screenBuffer.renderToConsole();
            if (checkForCollision(driver, obstacleManager))
            {
                isRunning = false;
                Console.ReadKey();
            }

            Thread.Sleep(timeout);

        }
    }

    public bool checkForCollision(Driver driver, ObstacleManager obstacleManager)
    {
        for (int i = 0; i < obstacleManager.obstacles.Count; i++)
        {
            Obstacle obstacle = obstacleManager.obstacles[i];

            if (obstacle.currentLane == driver.currentLane &&
                (obstacle.firstRowPosition + obstacle.carObstacleHeight + collisionOffset >= Utilities.screenHeight - driver.carHeight))
            {
                return true;
            }
        }
        return false;
    }
}

enum Option
{
    NewGame = 0,
    LoadGame = 1,
    Exit = 2
}

