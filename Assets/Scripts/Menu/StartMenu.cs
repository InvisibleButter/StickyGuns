using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public List<ViewModel> SubMenus = new List<ViewModel>();
    public GameObject StartMenuVisual;

    public void StartGame() 
    {
        StartMenuVisual.SetActive(false);
    }

    public void OpenCredits() 
    {
        SubMenus[0].Open();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
