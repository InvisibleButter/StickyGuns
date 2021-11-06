using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public List<ViewModel> SubMenus = new List<ViewModel>();

    public void StartGame() 
    {
        //todo - it would be really cool, if the menu is in the mainscene and the game starts directly without loading new scene
        SceneManager.LoadScene(1);
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
