using UnityEngine;

public class ViewModel : MonoBehaviour
{
    public GameObject Visual;

    public void Open()
    {
        Visual.SetActive(true);
    }

    public void Close()
    {
        Visual.SetActive(false);
    }
}
