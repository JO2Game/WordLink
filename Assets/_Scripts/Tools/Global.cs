using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局变量
/// </summary>
public class Global : MonoBehaviour
{
    public static string levelPath = Application.dataPath + "/Level/LevelData.Json"; //关卡数据
    public static int currentLevelID=1; //当前关卡ID

    //音频路径
    public static string startAudioPath = "Audio/Start";
    public static string buttonAudioPath = "Audio/Button";
    public static string wrongAudioPath = "Audio/Wrong";
    public static string successAudioPath = "Audio/Success";
    public static string winAudioPath = "Audio/Win";
}
