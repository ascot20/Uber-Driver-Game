using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Utilities;

class GameManager
{
    private const int minWindowWidth = 170;
    private const int minWindowHeight = 40;
    private const int defaultStartingLane = 2;
    private const int timeout = 33;
    private const string savedGamesFile = "Saved Games.json";
    private const string configFile = "config.json";

    public GameManager()
    {
        Console.CursorVisible = false;

        Screen.setupConsoleSize(minWindowWidth, minWindowHeight);

        int optionSelected = GameMenus.displayStartMenu(minWindowWidth, minWindowHeight);

        switch ((StartMenuOptions)optionSelected)
        {
            case StartMenuOptions.NewGame:
                this.loadNewGame();
                break;

            case StartMenuOptions.LoadGame:
                this.loadSavedGame();
                break;

            case StartMenuOptions.Exit:
                break;
        }
    }

    private Dictionary<string, int> loadConfig()
    {
        try
        {
            Dictionary<string, int> config = FileHelper.LoadData<Dictionary<string, int>>(configFile);
            return config;
        }
        catch
        {
            //create new config file if an error is encountered
            Dictionary<string,int> config = new Dictionary<string,int>();
            config[configKeys.StartingLane.ToString()] = defaultStartingLane;
            FileHelper.SaveData(config,configFile);

            return config;
        }
    }

    private void loadNewGame()
    {
        string username = GameMenus.displayNewGameMenu(minWindowWidth, minWindowHeight);
        Driver driver = new Driver(username, this.loadConfig()[configKeys.StartingLane.ToString()]);
        this.handleGamePlay(driver);
    }

    private void loadSavedGame()
    {
        try
        {
            Driver driver = FileHelper.LoadData<Driver>(savedGamesFile);
            this.handleGamePlay(driver);
        }
        catch
        {
            //Load new game if no data in saved game file
            this.loadNewGame();
        }
    }

    private void handleGamePlay(Driver driver)
    {
        GameEnvironmentVariables gameEnvironmentVariables;

        gameEnvironmentVariables.screenBuffer = new ScreenBuffer(Screen.screenWidth, Screen.screenHeight);
        gameEnvironmentVariables.environment = new Environment(gameEnvironmentVariables.screenBuffer);
        gameEnvironmentVariables.driver = driver;
        gameEnvironmentVariables.obstacleManager = new ObstacleManager();
        gameEnvironmentVariables.accountManager = new AccountManager(gameEnvironmentVariables.driver, gameEnvironmentVariables.screenBuffer);

        gameEnvironmentVariables.driver.deployCar(gameEnvironmentVariables.screenBuffer);

        Stopwatch stopwatch = new Stopwatch();

        bool isRunning = true;

        while (isRunning)
        {
            stopwatch.Restart();

            gameEnvironmentVariables.obstacleManager.addObstacle();
            gameEnvironmentVariables.obstacleManager.moveObstacles(gameEnvironmentVariables.driver,
                gameEnvironmentVariables.screenBuffer, gameEnvironmentVariables.accountManager);

            this.handleCarSteering(gameEnvironmentVariables);
            this.handleRendering(gameEnvironmentVariables);
            isRunning = continueGame(gameEnvironmentVariables);

            TimeSpan timeElapsed = stopwatch.Elapsed;

            if (timeElapsed.Milliseconds < timeout)
            {
                Thread.Sleep(timeout - timeElapsed.Milliseconds);
            }
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
            Screen.setupConsoleSize(minWindowWidth, minWindowHeight);
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

            FileHelper.SaveData(gameEnvironmentVariables.driver, savedGamesFile);

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
        gameEnvironmentVariables.accountManager.updateAccountDashboard(gameEnvironmentVariables.driver, gameEnvironmentVariables.screenBuffer);
    }
}