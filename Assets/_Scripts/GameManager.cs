using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Canvas canvas; //当前canvas
    public GameObject linepfb; //线段预制体
    public Transform lineContainer; //线段容器
    public bool isInit; //是否初始化完成
    public WordPanel wordPanel; //单词面板
    public CollectionPanel collectionPanel; //收集的单词面板
    public WinDialog winDialog; //游戏结束对话框
    private Letter[,] lettersArray; //存放字母对象的数组
    private Letter headLetter; 
    private Letter endLetter;
    private LineRenderer currentLine;
    private string[] wordArray; //存放给定单词的数组
    public List<Word> wordList; //存放给定单词对象的数组
    private int wordCount; //需要找到的单词数

    void Awake()
    {
        instance = this;
        wordList = new List<Word>();       
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (!isInit)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            SelectStart();
        }

        if (currentLine != null)
        {
             currentLine.SetPosition(1, GetMousePosition());
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectEnd();
        }

    }

    /// <summary>
    /// 将字母对象放入数组
    /// </summary>
    /// <param name="letter"></param>
    public void AddLetterToArray(Letter letter)
    {
        lettersArray[letter.position.x, letter.position.y] = letter;
    }

    /// <summary>
    /// 连接
    /// </summary>
    private void Link()
    {
        List<Letter> currentLetterList = new List<Letter>(); //存放要连接的字母
        string tempWord = string.Empty; //存放字母组成的单词
        if (IsRow(headLetter.position, endLetter.position)) //如果是一行，获取首尾字母之间的所有字母并进行判断
        {
            int n = endLetter.position.y - headLetter.position.y;
            n = n / Mathf.Abs(n);
            int y = headLetter.position.y;            
            for (int i=0;i<=Mathf.Abs(endLetter.position.y - headLetter.position.y);i++)
            {
                currentLetterList.Add(lettersArray[headLetter.position.x, y]);
                tempWord += lettersArray[headLetter.position.x, y].GetLetter();
                y += n;
            }
            Judge(currentLetterList,tempWord);
        }
        else if (IsCol(headLetter.position, endLetter.position)) //如果是一列
        {
            int n = endLetter.position.x - headLetter.position.x;
            n = n / Mathf.Abs(n);
            int x = headLetter.position.x;
            for (int i = 0; i <= Mathf.Abs(endLetter.position.x - headLetter.position.x); i++)
            {
                currentLetterList.Add(lettersArray[x, headLetter.position.y]);
                tempWord += lettersArray[x, headLetter.position.y].GetLetter();
                x += n;
            }
            Judge(currentLetterList, tempWord);
        }
        else if (IsDiag(headLetter.position, endLetter.position)) //如果是对角线
        {
            int n = endLetter.position.x - headLetter.position.x;
            n = n / Mathf.Abs(n);
            int m = endLetter.position.y - headLetter.position.y;
            m = m / Mathf.Abs(m);
            int x = headLetter.position.x;
            int y = headLetter.position.y;
            for (int i = 0; i <= Mathf.Abs(endLetter.position.x - headLetter.position.x); i++)
            {
                currentLetterList.Add(lettersArray[x, y]);
                tempWord += lettersArray[x, y].GetLetter();
                x += n;
                y += m;
            }
            Judge(currentLetterList, tempWord);
        }
        else //都不是就删除当前线段
        {
            DelLine();
        }

    }

    /// <summary>
    /// 判断是否与给定单词匹配
    /// </summary>
    /// <param name="list"></param>
    /// <param name="word"></param>
    private void Judge(List<Letter> list,string word)
    {
        int i = Array.IndexOf(wordArray, word);
        if (i == -1) 
        {
            AudioManager.instance.PlayAudio(AudioType.Wrong);
            foreach (var item in list)
            {
                item.PlayAnimation();
            }
            DelLine();
        }
        else
        {
            AudioManager.instance.PlayAudio(AudioType.Success);
            foreach (var item in list)
            {
                item.Selected();
            }
            wordList[i].Complete();
            wordArray[i] = string.Empty;
            wordCount--;
            if (wordCount == 0) //如果所有单词都被找到就赢得这一局
            {
                Win();
            }
        }
    }

    /// <summary>
    /// 胜利
    /// </summary>
    private void Win()
    {
        isInit = false;
        LineActive(false);
        bool res=LevelMag.SetLevelsStatus(Global.currentLevelID+1, true); //尝试解锁下一关，如果存在返回true，不存在就返回false
        winDialog.ShowWinPanel(res);
        AudioManager.instance.PlayAudio(AudioType.Win);
    }

    public void LineActive(bool act)
    {
        lineContainer.gameObject.SetActive(act);
    }

    //判断行列或者对角线
    private bool IsRow(Vector2Int v1,Vector2Int v2)
    {
        return v1.x == v2.x;
    }

    private bool IsCol(Vector2Int v1,Vector2Int v2)
    {
        return v1.y == v2.y;
    }

    private bool IsDiag(Vector2Int v1,Vector2Int v2)
    {
        return Mathf.Abs(v1.x - v2.x) == Mathf.Abs(v1.y - v2.y);
    }

    /// <summary>
    /// 选择第一个字母
    /// </summary>
    private void SelectStart()
    {
        List<RaycastResult> results = GetRaycast(); 
        if (headLetter == null && results.Count != 0)
        {
            headLetter = results[0].gameObject.GetComponent<Letter>();
            if (headLetter != null) //成功获取到第一个字母就实例化线段并将当前鼠标位置设为线段的第一个点
            {
                currentLine = Instantiate(linepfb,lineContainer).GetComponent<LineRenderer>();
                currentLine.SetPosition(0, GetMousePosition());
            }
        }
    }
    /// <summary>
    /// 获取最后一个字母
    /// </summary>
    private void SelectEnd()
    {
        List<RaycastResult> results = GetRaycast();
        if (headLetter != null && endLetter == null && results.Count != 0)
        {
            endLetter = results[0].gameObject.GetComponent<Letter>();
            if (endLetter != null && endLetter != headLetter) //如果成功获取到并且与第一个字母不同就连线
            {
                Link();
            }
            else
            {
                DelLine();
            }
        }
        else
        {
            DelLine();
        }
        headLetter = null;
        endLetter = null;
        currentLine = null;
    }

    /// <summary>
    /// 删除线段
    /// </summary>
    private void DelLine()
    {
        if (currentLine != null)
        {
            Destroy(currentLine.gameObject);
        }
    }

    /// <summary>
    /// 获取鼠标位置
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMousePosition()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;
        return temp;
    }

    /// <summary>
    /// 初始化场景
    /// </summary>
    private void Init()
    {
        List<LevelData> levelList = LevelMag.LoadLevels(); //加载关卡数据
        if (levelList.Count == 0)
        {
            return;
        }
        LevelData currentLevel = levelList[Global.currentLevelID - 1];
        wordArray=currentLevel.Words.Split(',');
        wordCount = wordArray.Length;
        int len =(int)Mathf.Sqrt(currentLevel.Letters.Length);
        lettersArray = new Letter[len,len];
        InitCollectionPanel(wordArray);
        InitWordPanel(currentLevel);
        AudioManager.instance.PlayAudio(AudioType.Start);
    }

    /// <summary>
    /// 初始化单词面板
    /// </summary>
    /// <param name="levelData"></param>
    private void InitWordPanel(LevelData levelData)
    {
        wordPanel.Init(levelData);
    }

    /// <summary>
    /// 初始化需要收集的单词面板
    /// </summary>
    /// <param name="words"></param>
    private void InitCollectionPanel(string[] words)
    {
        collectionPanel.Init(words);
    }

    /// <summary>
    /// 获取鼠标下方元素
    /// </summary>
    private List<RaycastResult> GetRaycast()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        return results;
    }


}
