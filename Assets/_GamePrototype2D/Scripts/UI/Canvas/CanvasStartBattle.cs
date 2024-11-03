using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CanvasStartBattle : CanvasBase
{
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private TextMeshProUGUI _countNumText;
    
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Reset()
    {
        fadeDuration = 3f;
    }

    public override void Show(object data = null)
    {
        base.Show(data);
        
        canvasGroup.alpha = 0f; // Initially invisible

        // Fade-in effect
        canvasGroup.DOFade(1f, fadeDuration / 2f);

        // Delay and fade-out
        DOTween.Sequence()
            .AppendInterval(fadeDuration / 2f)
            .Append(canvasGroup.DOFade(0f, fadeDuration / 2f))
            .Play();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
