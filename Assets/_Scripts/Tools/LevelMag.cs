using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// 关卡管理类
/// </summary>
public static class LevelMag 
{
    /// <summary>
    /// 加载关卡数据
    /// </summary>
    /// <returns></returns>
    public static List<LevelData> LoadLevels()
    {
        try
        {
            if (!File.Exists(Global.levelPath))
            {
                return null;
            }
            using (StreamReader file = new StreamReader(Global.levelPath))
            {
                string fileContents = file.ReadToEnd();
                LevelList levels = JsonMapper.ToObject<LevelList>(fileContents);
                return levels.Levels;
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
            return new List<LevelData>();
        }
    }

    /// <summary>
    /// 修改关卡锁定状态
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="unlock"></param>
    /// <returns></returns>
    public static bool SetLevelsStatus(int levelID, bool unlock)
    {
        try
        {
            LevelList levels = new LevelList();
            levels.Levels = LoadLevels();
            if (unlock)
            {
                levels.Levels[levelID - 1].Unlock = 1;
            }
            else
            {
                levels.Levels[levelID - 1].Unlock = 0;
            }
            string jsonStr = JsonMapper.ToJson(levels);
            string json = Regex.Unescape(jsonStr);
            using (StreamWriter file = new StreamWriter(Global.levelPath, false))
            {
                file.Write(json);
            }
            return true;
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }

}
