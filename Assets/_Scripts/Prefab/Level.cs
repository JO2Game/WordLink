using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public Button btn;
    public Image lockImg;
    public Text idText;
    public string lockColor = "#F7B731";
    public string unLockColor = "#20BF6B";
    private int id;

    void Awake()
    {
        btn.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        Global.currentLevelID = id;
        SceneManager.LoadScene("Main");
    }

    public void SetID(int id)
    {
        this.id = id;
        idText.text = id.ToString();
    }

    public int GetID()
    {
        return this.id;
    }

    public void SetLock(bool l)
    {
        lockImg.gameObject.SetActive(!l);
        btn.interactable = l;
        if (l)
        {
            this.GetComponent<Image>().color = GetColor(unLockColor);
        }
        else
        {
            this.GetComponent<Image>().color = GetColor(lockColor);
        }
    }

    private Color GetColor(string hex)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hex, out newColor))
        {
            return newColor;
        }
        else
        {
            return this.GetComponent<Image>().color;
        }
    }

}
