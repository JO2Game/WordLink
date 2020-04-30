using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopPanel : MonoBehaviour
{
    public Button backBtn;
    public ConfirmDialog dialog;
    
    void Awake()
    {
        backBtn.onClick.AddListener(OnBackBtnClick);
    }

    private void OnBackBtnClick()
    {
        AudioManager.instance.PlayAudio(AudioType.Button);
        GameManager.instance.LineActive(false);
        dialog.BindEnter(() => {
            SceneManager.LoadScene("Start");
        });
        dialog.BindCancel(()=> {
            AudioManager.instance.PlayAudio(AudioType.Button);
            GameManager.instance.LineActive(true);
        });
        dialog.SetText("Are you sure you want to quit?");      
        dialog.Show();
    }
}
