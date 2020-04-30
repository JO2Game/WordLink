using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmDialog : DialogBase
{
    public Button enterBtn;
    public Button cancelBtn;
    public Text text;

    private Action enterAction;
    private Action cancelAction;
    
    void Awake()
    {    
        cancelBtn.onClick.AddListener(OnCancelBtnClick);
        enterBtn.onClick.AddListener(OnEnterBtnClick);
    }

    public void BindEnter(Action action)
    {
        enterAction = action;
    }

    public void BindCancel(Action action)
    {
        cancelAction = action;
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    private void OnCancelBtnClick()
    {
        Hide();
        if (cancelAction != null)
        {
            cancelAction();
        }
    }

    private void OnEnterBtnClick()
    {
        if (enterAction != null)
        {
            enterAction();
        }
    }
}
