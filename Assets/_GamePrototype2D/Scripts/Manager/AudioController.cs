using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
[RequireComponent(typeof(Initialization))]
public class AudioController : Singleton<AudioController>
{
    public const string MUSIC_STATUS = "music_status";
    public const string SFX_STATUS = "sfx_status";
    [SerializeField] private AudioSource sfxSource, musicSource;
    public AudioClip fireplaceAudio;
    public AudioClip bumTesla;
    private Dictionary<AudioClip, float> clipTimestampCollection;

    [Header("DialogClip")]
    public List<AudioClip> dialogClips;

    public void PlayRandomDialogClip()
    {
        int random = Random.Range(0, 3);
        PlaySfx(dialogClips[random]);
    }
    private void OnEnable()
    {
        musicSource.volume = MusicVolume;
        sfxSource.volume = SFXVolume;
        clipTimestampCollection = new Dictionary<AudioClip, float>();
    }
    protected override void OnRegistration()
    {
        musicSource.Play();
    }
    public void PlaySfx(AudioClip clip, bool shouldCheckTimestamp = false)
    {
        if (SFXVolume > 0)
        {
            if (shouldCheckTimestamp)
            {
                if (clipTimestampCollection.ContainsKey(clip) && Time.time - clipTimestampCollection[clip] < clip.length) return;
                clipTimestampCollection[clip] = Time.time;
            }
            sfxSource.PlayOneShot(clip);
        }
    }
    public void PlayLongSFX(AudioClip clip, bool isLoop = false)
    {
        if (SFXVolume > 0)
        {
            sfxSource.clip = clip;
            sfxSource.loop = isLoop;
            sfxSource.Play();
        }
    }
    public void StopLongSFX()
    {
        sfxSource.Stop();
    }
    public void PlayMusic(AudioClip clip, bool isLoop = true)
    {
        if (musicSource.clip != null)
            if (musicSource.clip.name == clip.name)
                return;
        musicSource.clip = clip;
        musicSource.loop = isLoop;
        if (SFXVolume > 0)
            musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }
    public float MusicVolume
    {
        get { return PlayerPrefs.GetFloat(MUSIC_STATUS, musicSource.volume); }
        set
        {
            PlayerPrefs.SetFloat(MUSIC_STATUS, Mathf.Clamp(value, 0f, 1f));
            musicSource.volume = PlayerPrefs.GetFloat(MUSIC_STATUS, musicSource.volume);
        }
    }
    public float SFXVolume
    {
        get
        { return PlayerPrefs.GetFloat(SFX_STATUS, sfxSource.volume); }
        set
        {
            PlayerPrefs.SetFloat(SFX_STATUS, Mathf.Clamp(value, 0f, 1f));
            sfxSource.volume = PlayerPrefs.GetFloat(SFX_STATUS, sfxSource.volume);
        }
    }

    public IEnumerator PlayIngameMusic(AudioClip clip, bool isRunning, int stopTime)
    {
        while (isRunning)
        {
            PlayMusic(clip, false);
            yield return new WaitForSeconds(stopTime);
        }
    }

}
