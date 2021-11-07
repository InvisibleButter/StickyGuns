using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public Image NumberImg;

    public void Setup(int number)
    {
        gameObject.SetActive(true);
        NumberImg.sprite = Sprites[number - 1];
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
