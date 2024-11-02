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
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _pressed;
    
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
    
    
    private Vector3 _originalLocalScale;
    
    private void Awake() 
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        
        _originalLocalScale = this.transform.localScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
        transform.localScale = _originalLocalScale;
         DOTween.Sequence().Append(transform.DOScale(_originalLocalScale, 0.15f))
             .OnComplete(() =>
             {
                 // _image.sprite = _default;
             });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // AudioManager.Instance.Play(_clickSound);
        transform.localScale = _originalLocalScale * _pointerClickScale;
        DOTween.Sequence()
            .Append(transform.DOScale(_originalLocalScale * _pointerClickScale, 0.15f))
            .OnComplete((() =>
            {
                // _image.sprite = _pressed;
            }));
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

