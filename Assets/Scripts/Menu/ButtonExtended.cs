using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonExtended : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image ButtonImg;

    public Sprite DefaultSprite, HighlighedSprite, PressedSprite;

    private bool _isHovered;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_isHovered)
        {
            ButtonImg.sprite = PressedSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isHovered)
        {
            ButtonImg.sprite = HighlighedSprite;
            _isHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isHovered)
        {
            ButtonImg.sprite = DefaultSprite;
            _isHovered = false;
        }
    }
}
