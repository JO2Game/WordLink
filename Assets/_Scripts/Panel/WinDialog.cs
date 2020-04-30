using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinDialog : DialogBase
{
    public Button nextBtn;
    public Button backBtn;
    
    void Awake()
    {
        nextBtn.onClick.AddListener(OnNextBtnClick);
        backBtn.onClick.AddListener(OnBackBtnClick);
    }

    private void OnBackBtnClick()
    {
        SceneManager.LoadScene("Start");
    }

    private void OnNextBtnClick()
    {
        Global.currentLevelID++;
        SceneManager.LoadScene("Main");
    }

    public void ShowWinPanel(bool next=true)
    {
        Show();
        nextBtn.interactable = next;
    }
}
