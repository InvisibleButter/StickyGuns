using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : ViewModel
{
    public TMP_Text ScoreText;
    public override void Open()
    {
        base.Open();
        //getscore
        ScoreText.text = "999999";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
