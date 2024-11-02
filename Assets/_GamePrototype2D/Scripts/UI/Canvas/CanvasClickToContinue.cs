using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CanvasClickToContinue : CanvasBase
{
    [SerializeField] private float _hideTimeDuration = 0.7f;
    private Vector2 _moveDistance = new Vector2(0f, 50f);
    public Canvas Canvas;
    
    public float moveDownDistance = 100f;
    public float moveUpDistance = 300f;
    public float moveDuration = 0.5f;
    private bool isMoving = false;
    private void OnEnable()
    {
        // _self = GetComponent<RectTransform>();
        
        EventTrigger trigger = GetComponent<EventTrigger>();
        if (trigger == null)
        {
            Debug.LogError($"TNam - EventTrigger is null");
            trigger = gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerClick
        };
        
        entry.callback.AddListener(HandlePointerClick); 
    
        trigger.triggers.Add(entry);
    }
    
    void HandlePointerClick(BaseEventData eventData)
    {
        Debug.LogError($"TNam - this is called");
        if (!isMoving)
        {
            isMoving = true;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(Canvas.transform.DOMoveY(Canvas.transform.position.y - moveDownDistance, moveDuration));
            sequence.Append(Canvas.transform.DOMoveY(Canvas.transform.position.y + moveUpDistance, moveDuration));
            sequence.Play().OnComplete(() => isMoving = false);
        }
    }

    public override void Show(object data = null)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        // _self.DOMove(_self.anchoredPosition + _moveDistance, _hideTimeDuration, true).OnComplete((() => base.Hide()));
        
        
    }
}
