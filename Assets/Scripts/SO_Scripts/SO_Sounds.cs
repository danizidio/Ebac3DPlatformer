using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound Manager", menuName = "Sound Atributes", order = 0)]
public class SO_Sounds : ScriptableObject
{
    [SerializeField] AudioMixer _mixerGroup;
    public AudioMixer mixerGroup { get { return _mixerGroup; } }

    [SerializeField] List<MusicSetup> _musicSetupList;
    public List<MusicSetup> musicSetupList { get { return _musicSetupList; } }
    
    [SerializeField] List<SFXSetup> _sfxSetupList;
    public List<SFXSetup> sfxSetupList { get { return _sfxSetupList; } }    
}

    [Serializable]
    public class MusicSetup
    {
        [SerializeField] MusicType _musicType;
        public MusicType musicType { get { return _musicType; } }
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

