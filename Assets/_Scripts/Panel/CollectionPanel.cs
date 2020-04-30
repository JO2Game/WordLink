using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单词收集面板
/// </summary>
public class CollectionPanel : MonoBehaviour
{
    public GameObject wordPfb;
    public GridLayoutGroup wordContainer;
    
    public void Init(string[] words)
    {
        wordContainer.cellSize = new Vector2(wordContainer.GetComponent<RectTransform>().sizeDelta.x / words.Length, 100);
        CreateWord(words);
    }

    private void CreateWord(string[] words)
    {
        foreach (string word in words)
        {
            GameObject go = Instantiate(wordPfb, wordContainer.transform);
            Word tempWord = go.GetComponent<Word>();
            tempWord.SetWord(word);
            GameManager.instance.wordList.Add(tempWord);
        }
    }

}
