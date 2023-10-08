using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : GenericSingleton<BGMManager>
{
    [SerializeField] AudioClip[] _bgms;
    AudioSource _audioSource;
    public void Init()
    {
        _audioSource = GetComponent<AudioSource>();
        GenericSingleton<UIBase>.Instance.MusicVolume += Sound;
        Sound(PlayerPrefs.GetFloat("MusicVolume"));
        SetBgm(0);
    }
    public void SetBgm(int stage)
    {
        _audioSource.clip = _bgms[stage];
        _audioSource.Play();
    }
    public void Sound(float volume)
    {
        _audioSource.volume = volume;
    }

}
