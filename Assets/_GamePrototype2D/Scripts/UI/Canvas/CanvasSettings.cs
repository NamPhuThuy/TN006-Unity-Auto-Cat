using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.VFX;

public class CanvasSettings : CanvasBase
{
    [SerializeField] private AudioMixer _myMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _masterSlider;
    private string PP_volumeMusic = "volumeMusic", PP_volumeMaster = "volumeMaster", PP_volumeSFX = "volumeSFX"; 
    
    
    private void Start()
    {
        LoadVolumeValue();
        SetVolume();
    }

    public void SetVolume()
    {
        SetVolumeMusic();
        SetVolumeSFX();
        SetVolumeMaster();
    }

    public void SetVolumeMusic()
    {
        float volumeMusic = _musicSlider.value;
        // AudioManager.Instance.volumeMusic = volumeMusic;
        // PlayerPrefs.SetFloat(PP_volumeMusic, volumeMusic);
        _myMixer.SetFloat("volumeMusic", Mathf.Log10(volumeMusic) * 20);
    }

    public void SetVolumeMaster()
    {
        float volumeMaster = _masterSlider.value;
        // AudioManager.Instance.volumeMaster = volumeMaster;
        // PlayerPrefs.SetFloat(PP_volumeMaster, volumeMaster);
        _myMixer.SetFloat("volumeMaster", Mathf.Log10(volumeMaster) * 20);


    }

    public void SetVolumeSFX()
    {
        float volumeSFX = _sfxSlider.value;
        // AudioManager.Instance.volumeSFX = volumeSFX;
        // PlayerPrefs.SetFloat(PP_volumeSFX, volumeSFX);
        _myMixer.SetFloat("volumeSFX", Mathf.Log10(volumeSFX) * 20);
    }

    public void LoadVolumeValue()
    {
        // _sfxSlider.value = PlayerPrefs.GetFloat(PP_volumeSFX, 0f);
        // _musicSlider.value = PlayerPrefs.GetFloat(PP_volumeMusic, 0f);
        // _masterSlider.value = PlayerPrefs.GetFloat(PP_volumeMaster, 0f);
        
        // _sfxSlider.value = AudioManager.Instance.volumeSFX;
        // _musicSlider.value = AudioManager.Instance.volumeMusic;
        // _masterSlider.value = AudioManager.Instance.volumeMaster;
    }
    
    
}
