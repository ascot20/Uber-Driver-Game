using System;
using System.Threading;
using Utilities;

class GameManager
{
    private int timeout = 15;

    public GameManager()
    {

        Console.CursorVisible = false;

        Screen.setupConsoleSize();

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
        GameEnvironmentVariables gameEnvironmentVariables;

        gameEnvironmentVariables.screenBuffer = new ScreenBuffer(Screen.screenWidth, Screen.screenHeight);
        gameEnvironmentVariables.environment = new Environment(gameEnvironmentVariables.screenBuffer);
        gameEnvironmentVariables.driver = new Driver(username, gameEnvironmentVariables.screenBuffer);
        gameEnvironmentVariables.obstacleManager = new ObstacleManager();
        gameEnvironmentVariables.accountManager = new AccountManager(gameEnvironmentVariables.driver, gameEnvironmentVariables.screenBuffer);

        bool isRunning = true;

        while (isRunning)
        {
            gameEnvironmentVariables.obstacleManager.addObstacle();
            gameEnvironmentVariables.obstacleManager.moveObstacles(gameEnvironmentVariables.driver,
                gameEnvironmentVariables.screenBuffer, gameEnvironmentVariables.accountManager);

            handleCarSteering(gameEnvironmentVariables);
            handleRendering(gameEnvironmentVariables);
            isRunning = continueGame(gameEnvironmentVariables);
 
            Thread.Sleep(timeout);

        }
    }

    private void handleRendering(GameEnvironmentVariables gameEnvironmentVariables)
    {
        try
        {
            gameEnvironmentVariables.screenBuffer.renderToConsole();
        }
        catch
        {
            Screen.setupConsoleSize();
            gameEnvironmentVariables.screenBuffer.setScreenBufferDimension(Screen.screenWidth, Screen.screenHeight);
            resetGame(gameEnvironmentVariables);
        }
    }

    private void handleCarSteering(GameEnvironmentVariables gameEnvironmentVariables)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A:
                    gameEnvironmentVariables.driver.steerLeft(gameEnvironmentVariables.screenBuffer);
                    break;

                case ConsoleKey.D:
                    gameEnvironmentVariables.driver.steerRight(gameEnvironmentVariables.screenBuffer);
                    break;

            }
        }
    }

    private bool continueGame(GameEnvironmentVariables gameEnvironmentVariables)
    {
        if (gameEnvironmentVariables.obstacleManager.checkForCollision(gameEnvironmentVariables.driver))
        {
            gameEnvironmentVariables.accountManager.deductAccount(gameEnvironmentVariables.driver, gameEnvironmentVariables.screenBuffer);
            bool continueGame = GameMenus.displayCollisionMenu(gameEnvironmentVariables.screenBuffer);

            if (continueGame)
            {
                this.resetGame(gameEnvironmentVariables);
                return true;
            }

            else
            {
                return false;
            }
        }

        return true;
    }

    private void resetGame(GameEnvironmentVariables gameEnvironmentVariables)
    {
        gameEnvironmentVariables.obstacleManager.clearObstacles();
        gameEnvironmentVariables.screenBuffer.clearBuffer();
        Screen.clearConsole();
        
        gameEnvironmentVariables.driver.deployCar(gameEnvironmentVariables.screenBuffer);
        gameEnvironmentVariables.environment.drawEnvironment(gameEnvironmentVariables.screenBuffer);
        gameEnvironmentVariables.accountManager.updateAccountDashboard(gameEnvironmentVariables.driver,
            gameEnvironmentVariables.screenBuffer);
    }
}

