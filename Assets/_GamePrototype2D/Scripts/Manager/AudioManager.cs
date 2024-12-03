using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioSource _audioSource;
    void Start()
    {
        _audioSource.clip = _backgroundMusic;
        _audioSource.Play();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
