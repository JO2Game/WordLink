using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单词面板
/// </summary>
public class WordPanel : MonoBehaviour
{
    public GameObject letterPfb;
    public float createSpeed = 1;
    private GridLayoutGroup glg;

    private void Awake()
    {
        glg = this.GetComponent<GridLayoutGroup>();
    }

    public void Init(LevelData levelData)
    {
        float cellSize = this.GetComponent<RectTransform>().sizeDelta.x / Mathf.Sqrt(levelData.Letters.Length) -1;
        glg.cellSize = new Vector2(cellSize, cellSize);
        CreateLetter(levelData.Letters);
    }

    private void CreateLetter(string letters)
    {
        StartCoroutine(AddLetter(letters));
    }

     IEnumerator AddLetter(string letters)
    {
        Queue<Letter> temp = new Queue<Letter>();
        foreach(char item in letters)
        {
            GameObject go = Instantiate(letterPfb, this.transform);
            Letter letter = go.GetComponent<Letter>();
            letter.SetLetter(item);
            temp.Enqueue(letter);
            yield return new WaitForSeconds(1/createSpeed);
        }
        int len = (int)Mathf.Sqrt(letters.Length);
        for (int i = 0; i < len; i++)
        {
            for(int j = 0; j < len; j++)
            {
                Letter tempLetter = temp.Dequeue();
                tempLetter.position.x = i;
                tempLetter.position.y = j;
                GameManager.instance.AddLetterToArray(tempLetter);
                //Debug.Log(tempLetter.position);
            }
            GameManager.instance.isInit = true;
        }
    }


}
