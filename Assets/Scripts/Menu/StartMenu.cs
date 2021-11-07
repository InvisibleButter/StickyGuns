using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public List<ViewModel> SubMenus = new List<ViewModel>();
    public GameObject StartMenuVisual;

    public GameObject ButtonsHolder, Logo;

    public bool OnPause;
    public bool InStartMenu;

    private void Awake()
    {
        Time.timeScale = 1;
        OnPause = false;
        InStartMenu = true;

        SetStartMenuStuff(true);
    }

    public void StartGame() 
    {
        StartMenuVisual.SetActive(false);
        InStartMenu = false;
    }

    public void OpenCredits() 
    {
        SubMenus[0].Open();
        SetStartMenuStuff(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(InStartMenu)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !OnPause)
        {
            OnPause = true;
            SubMenus[1].Open();
        }
    }

    public void SetStartMenuStuff(bool b)
    {
        ButtonsHolder.SetActive(b);
        Logo.SetActive(b);
    }
}
