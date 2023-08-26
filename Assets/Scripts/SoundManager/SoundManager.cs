using CommonMethodsLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] List<MusicSetup> _musicSetupList;
    [SerializeField]List<SFXSetup> _sfxSetupList;

    public void PlayMusicByType(MusicType type)
    {
        var music = GetMusicByType(type);
        _audioSource.clip = music.audioClip;
        _audioSource.Play();
    }

    MusicSetup GetMusicByType(MusicType type)
    {
        return _musicSetupList.Find(item => item.musicType == type);
    }

    SFXSetup GetSfxByType(SfxType type)
    {
        return _sfxSetupList.Find(item => item.sfxType == type);
    }
}

[Serializable]
public class MusicSetup
{
    [SerializeField] MusicType _musicType;
    public MusicType musicType { get { return _musicType;} }
    [SerializeField] AudioClip _audioClip;
    public AudioClip audioClip { get { return _audioClip; } }
}

[Serializable]
public class SFXSetup
{
    [SerializeField] SfxType _sfxType;
    public SfxType sfxType { get { return _sfxType; } }
    [SerializeField] AudioClip _audioClip;
    public AudioClip audioClip { get { return _audioClip; } }
}
