using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{

    public Transform levelContainer;
    public GameObject levelPfb;
    

    void Start()
    {
        Init();
    }

    private void Init()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        List<LevelData> list = LevelMag.LoadLevels();
        if (list != null && list.Count != 0)
        {
            StartCoroutine(CreateLevel(list));
        }
    }

    IEnumerator CreateLevel(List<LevelData> list)
    {
        foreach(var item in list)
        {
            Level level = Instantiate(levelPfb, levelContainer).GetComponent<Level>();
            level.SetID(item.ID);
            int l = item.Unlock;
            if (l ==1)
            {
                level.SetLock(true);
            }
            else
            {
                level.SetLock(false);
            }
            AudioManager.instance.PlayAudio(AudioType.Button);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
