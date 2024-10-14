using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonClicky : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [Header("Visual related")]
    [SerializeField] private ToggleFX _toggleFX;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private ButtonResourcesList _buttonResourcesList;
    [SerializeField] private int _defaultSpriteID = 0, _pressedSpriteID = 0;
    private Image _image;
    
    [Space(10)]
    [Header("Scales")]
    [SerializeField] private float _pointerHoverScale = 1.1f;
    [SerializeField] private float _pointerReleaseScale = 1f;
    [SerializeField] private float _pointerClickScale = 0.9f;

    [Space(10)]
    [Header("Audio Clips")]
    // [SerializeField] private AudioEnum _clickSound;
    // [SerializeField] private AudioEnum _hoverSound;
    
    private RectTransform _rectTransform;
    private float _changeY = 5.6f;
    
    private enum ToggleFX
    {
        DownToUp,
        SmallToBig
    }
    
    private Vector3 _originalLocalScale;
    
    private void Awake() 
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        
        _default = _buttonResourcesList._defaultButtonSprites[_defaultSpriteID];
        _pressed = _buttonResourcesList._pressedButtonSprites[_pressedSpriteID];
        _image.sprite = _default;
        
        _originalLocalScale = this.transform.localScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        switch (_toggleFX)
        {
            case ToggleFX.DownToUp:
                Vector2 anchoredPosition = _rectTransform.anchoredPosition;
                anchoredPosition.y += _changeY;
                _rectTransform.anchoredPosition = anchoredPosition;
                _image.sprite = _default;
                break;
            case ToggleFX.SmallToBig:
                transform.localScale = _originalLocalScale;
                DOTween.Sequence().Append(transform.DOScale(_originalLocalScale, 0.15f))
                    .OnComplete(() => { _image.sprite = _default; });
                break;
        }
        
            
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // AudioManager.Instance.Play(_clickSound);
        
        switch (_toggleFX)
        {
            case ToggleFX.DownToUp:
                Vector2 anchoredPosition = _rectTransform.anchoredPosition;
                anchoredPosition.y -= _changeY;
                _rectTransform.anchoredPosition = anchoredPosition;
                _image.sprite = _pressed;
                break;
            case ToggleFX.SmallToBig:
                transform.localScale = _originalLocalScale * _pointerClickScale;
                DOTween.Sequence()
                    .Append(transform.DOScale(_originalLocalScale * _pointerClickScale, 0.15f))
                    .OnComplete((() => { _image.sprite = _pressed;}));
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Module.MediumVibrate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // AudioManager.Instance.Play(_hoverSound);
        DOTween.Sequence().Append(transform.DOScale(_originalLocalScale * _pointerHoverScale, 0.2f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Sequence().Append(transform.DOScale(_originalLocalScale, 0.2f));
    }

    
    // detect and handle events when a pointer (e.g., mouse cursor or touch) moves over a UI element
    public void OnPointerMove(PointerEventData eventData)
    {
        // transform.localScale = localScaleOld * _pointerHoverScale;
    }
}

