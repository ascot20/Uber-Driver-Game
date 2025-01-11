enum StartMenuOptions
{
    NewGame = 0,
    LoadGame = 1,
    Exit = 2
}

struct GameEnvironmentVariables
{
    public Driver driver;
    public ScreenBuffer screenBuffer;
    public Environment environment;
    public ObstacleManager obstacleManager;
    public AccountManager accountManager;
}

struct BufferChar
{
    public int xPos;
    public int yPos;
    public char character;
}

struct BufferString
{
    public int xPos;
    public int yPos;
    public string text;
}
