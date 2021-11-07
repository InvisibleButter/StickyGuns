using UnityEngine;

public class ViewModel : MonoBehaviour
{
    public GameObject Visual;

    public virtual void Open()
    {
        Visual.SetActive(true);
    }

    public virtual void Close()
    {
        Visual.SetActive(false);
    }
}
