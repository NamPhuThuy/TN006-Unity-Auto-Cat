using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextFlicker : MonoBehaviour
{
    public TextMeshProUGUI textToFlicker;
    public float minAlpha = 0.4f;
    public float maxAlpha = 1f;
    public float flickerSpeed = 5f;

    private float currentAlpha = 1f;
    private bool isFlickering = true;

    void Update()
    {
        if (isFlickering)
        {
            currentAlpha += flickerSpeed * Time.deltaTime;

            if (currentAlpha > maxAlpha)
            {
                currentAlpha = maxAlpha;
                isFlickering = false;
            }

            textToFlicker.color = new Color(textToFlicker.color.r, textToFlicker.color.g, textToFlicker.color.b, currentAlpha);
        }
        else
        {
            currentAlpha -= flickerSpeed * Time.deltaTime;

            if (currentAlpha < minAlpha)
            {
                currentAlpha = minAlpha;
                isFlickering = true;
            }

            textToFlicker.color = new Color(textToFlicker.color.r, textToFlicker.color.g, textToFlicker.color.b, currentAlpha);
        }
    }
}