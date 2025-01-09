using System;
using System.Threading;

class GameManager
{
    //fields
    int timeout = 40;

    public GameManager()
    {

        Console.CursorVisible = false;

        Utilities.checkConsoleSize();

        int optionSelected = GameMenus.displayStartMenu();

        switch ((StartMenuOptions)optionSelected)
        {
            case StartMenuOptions.NewGame:
                string username = GameMenus.displayNewGameMenu();
                handleGamePlay(username);
                break;

            case StartMenuOptions.LoadGame:
                Console.WriteLine("Load game");
                break;

            case StartMenuOptions.Exit:
                Console.WriteLine("Exit game");
                break;
        }
    }


    private void handleGamePlay(string username)
    {
        ScreenBuffer screenBuffer = new ScreenBuffer(Utilities.screenWidth, Utilities.screenHeight);
        Environment environment = new Environment(screenBuffer);
        Driver driver = new Driver(username, screenBuffer);
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

            if (obstacleManager.checkForCollision(driver))
            {
                bool continueGame = GameMenus.displayCollisionMenu(screenBuffer);

                if (continueGame) 
                { 
                    this.resetGame(driver,environment,screenBuffer,obstacleManager);
                }

                else
                {
                    isRunning = false;
                }

            }

            Thread.Sleep(timeout);

        }
    }

    private void resetGame(Driver driver, Environment environment, ScreenBuffer screenBuffer, ObstacleManager obstacleManager)
    {
        obstacleManager.clearObstacles();
        screenBuffer.clearBuffer();
        Console.Clear();

        driver.deployCar(screenBuffer);
        environment.drawEnvironment(screenBuffer);
    }
}

enum StartMenuOptions
{
    NewGame = 0,
    LoadGame = 1,
    Exit = 2
}