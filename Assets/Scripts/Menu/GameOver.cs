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
        ScoreText.text = ScoreManager.Instance.Score.ToString();
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
