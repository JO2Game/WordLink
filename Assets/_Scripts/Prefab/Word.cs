using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

/// <summary>
/// 单词类
/// </summary>
public class Word : MonoBehaviour
{
    public Text wordText;
    private string word;
    public string originColor="#F7B731";
    public string completeColor = "#FC5C65";
    private Image image;

    private void Awake()
    {
        image = this.GetComponent<Image>();
    }

    public void SetWord(string word)
    {
        this.word = word;
        wordText.text = word;
    }

    public void Complete()
    {
        PlayAnimation(()=>{
            this.GetComponent<Image>().color = GetColor(completeColor);
        });
    }

    private void PlayAnimation(Action action=null)
    {
        this.GetComponent<RectTransform>().DOShakeAnchorPos(0.1f,5,10,0).SetLoops(5,LoopType.Yoyo).OnComplete(()=> {
            if (action != null)
            {
                action();
            }
        });
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
            return image.color;
        }
    }

}
