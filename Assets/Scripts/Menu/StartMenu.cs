using System.Collections.Generic;
using StickyGuns.Sound;
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
        AudioManager.Instance.Play("buttonRelease");
        StartMenuVisual.SetActive(false);
        InStartMenu = false;

        GameManager.Instance.StartGame();
    }

    public void OpenCredits() 
    {
        AudioManager.Instance.Play("buttonRelease");
        SubMenus[0].Open();
        SetStartMenuStuff(false);
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play("buttonRelease");
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

    public void ShowLoose()
    {
        SubMenus[2].Open();
    }
}
