using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int totalGold;
    public int totalDiamond;
    public int lastLevel;
    public List<LevelState> levelStates = new List<LevelState>();
}

[Serializable]
public class LevelState
{
    public int level;
    public int starCount;
    public bool isComplete;

    public LevelState()
    {

    }
    public LevelState(int level, bool isComplete, int starCount)
    {
        this.level = level;
        this.isComplete = isComplete;
        this.starCount = starCount;
    }
}
[Serializable]
public class WorldState
{
    public int worldId;
}
