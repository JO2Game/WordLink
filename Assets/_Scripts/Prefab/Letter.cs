using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 字母类
/// </summary>
public class Letter : MonoBehaviour
{
    public Text letterText;
    private string letter;
    public Vector2Int position;
    public string originColor = "#A55EEA";
    public string selectColor = "#F1BBF1";
    private Image image;

    private void Awake()
    {
        image = this.GetComponent<Image>();
    }

    public void SetLetter(char letter)
    {
        this.letter = letter.ToString();
        letterText.text = letter.ToString();
    }

    public string GetLetter()
    {
        return this.letter;
    }

    public void Selected()
    {
        image.color = GetColor(selectColor);
    }

    public void PlayAnimation(Action action=null)
    {
        image.DOColor(GetColor(selectColor), 0.2f).SetLoops(2,LoopType.Yoyo).OnComplete(()=> {
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
