using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    public TMPro.TMP_Text text;

    void Start()
    {
        ScoreManager.Instance.OnChange += UpdateScore; 
    }

    private void UpdateScore()
    {
        text.text = ScoreManager.Instance.Score.ToString();
    }
}
