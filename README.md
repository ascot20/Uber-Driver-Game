# Uber Driver Game

A console-based arcade driving game built in C# where you navigate through traffic, avoid collisions, and earn money as an Uber driver.

## Gameplay

Control your car on a 3-lane road while dodging oncoming traffic. Successfully avoiding obstacles earns you money, while collisions result in costly repairs.

### Controls

| Key | Action |
|-----|--------|
| `A` | Steer left |
| `D` | Steer right |
| `Y/N` | Continue/quit after collision |
| Arrow keys | Navigate menus |
| Enter | Select menu option |

### Scoring

- **Earnings**: £10-100 for each obstacle avoided
- **Repairs**: £200-500 deducted per collision

## Features

- Real-time obstacle avoidance gameplay
- Three-lane road system
- Persistent save/load system (JSON)
- ASCII art graphics with flicker-free rendering
- Player account tracking with earnings history

## Requirements

- .NET 8.0 SDK
- Terminal with minimum 170x40 character size

## Running the Game

```bash
cd UberDriverGame
dotnet run
```

## Project Structure

```
UberDriverGame/
├── Program.cs          # Entry point
├── GameManager.cs      # Main game loop and state management
├── Driver.cs           # Player car class
├── Obstacle.cs         # Enemy vehicle class
├── ObstacleManager.cs  # Obstacle spawning and movement
├── Environment.cs      # Road and lane rendering
├── AccountManager.cs   # Earnings and repair costs
├── ScreenBuffer.cs     # Double-buffered rendering system
├── GameMenus.cs        # Menu screens and UI
├── Types.cs            # Data structures and enums
└── Utilities.cs        # Helper classes (Screen, Text, FileHelper)
```

## Technical Details

- **Rendering**: Double-buffered with delta updates (only changed cells are redrawn)
- **Frame Rate**: ~30 FPS (33ms per frame)
- **Persistence**: Uses `System.Text.Json` for save/load functionality
- **Collision Detection**: Lane and vertical position checking
