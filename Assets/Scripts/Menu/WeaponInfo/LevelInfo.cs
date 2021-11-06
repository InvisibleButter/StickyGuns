using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public Image NumberImg;

    public void Setup(int number)
    {
        NumberImg.sprite = Sprites[number - 1];
    }
}
