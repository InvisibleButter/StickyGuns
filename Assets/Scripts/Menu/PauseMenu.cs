using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : ViewModel
{
    public StartMenu Menu;
    public override void Open()
    {
        base.Open();

        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1;
        Menu.OnPause = false;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
