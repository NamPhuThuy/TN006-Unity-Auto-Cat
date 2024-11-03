using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperAudio : MonoBehaviour
{
    public AudioClip clip;
    [SerializeField] private AudioSource audioSource;
    private void OnEnable()
    {
        if (GetComponent<Button>())
            GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        if (GetComponent<Button>())
            GetComponent<Button>().onClick.RemoveListener(OnClick);
    }
    public void OnClick()
    {
        AudioController.Instance.PlaySfx(clip);
    }
    public void PlayMusic()
    {
        AudioController.Instance.PlayMusic(clip);
    }
    public void PlayLoopAudio()
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopLongAudio()
    {
        AudioController.Instance.StopLongSFX();
    }
}
