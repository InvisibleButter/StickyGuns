using System.Collections.Generic;
using StickyGuns.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public List<ViewModel> SubMenus = new List<ViewModel>();
    public GameObject StartMenuVisual;

    public void StartGame() 
    {
        AudioManager.Instance.Play("buttonRelease");
        StartMenuVisual.SetActive(false);
    }

    public void OpenCredits() 
    {
        AudioManager.Instance.Play("buttonRelease");
        SubMenus[0].Open();
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play("buttonRelease");
        Application.Quit();
    }
}
