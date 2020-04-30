using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频管理类
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Range(0,1f)]
    public float volume=0.5f;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudio(AudioType type)
    {
        switch (type)
        {
            case AudioType.Start:
                Play(Global.startAudioPath);
                break;
            case AudioType.Button:
                Play(Global.buttonAudioPath);
                break;
            case AudioType.Wrong:
                Play(Global.wrongAudioPath);
                break;
            case AudioType.Success:
                Play(Global.successAudioPath);
                break;
            case AudioType.Win:
                Play(Global.winAudioPath);
                break;
            default:
                break;
        }
    }

    private void Play(string path)
    {
        AudioClip temp = Resources.Load(path) as AudioClip;
        if (temp != null)
        {
            AudioSource.PlayClipAtPoint(temp, this.transform.position, volume);
        }
    }

}

public enum AudioType
{
    Start,
    Button,
    Wrong,
    Success,
    Win,
}
