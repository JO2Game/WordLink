using UnityEngine;

public class DialogBase : MonoBehaviour
{
    public virtual void  Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
