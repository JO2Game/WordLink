using System;
using System.Collections.Generic;

[Serializable]
public class LevelList
{
    public List<LevelData> Levels;

    public LevelList()
    {
        Levels = new List<LevelData>();
    }
}
