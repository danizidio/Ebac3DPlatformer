using CommonMethodsLibrary;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] SO_Sounds _sounds;
    public SO_Sounds So_Sounds {  get { return _sounds; } }

    [SerializeField] AudioSource _audioSource;

    [SerializeField] GameObject _musicPlayer;
    public GameObject musicPlayer { get { return _musicPlayer; } }

    public void PlayMusicByType(MusicType type)
    {
        if(_audioSource == null)
        {
            MusicPlayer m = GameObject.FindAnyObjectByType<MusicPlayer>();
            if (m != null)
            {
                _audioSource = m.GetComponent<AudioSource>();
            }
            else
            {
                Instantiate(_musicPlayer);
                _audioSource = _musicPlayer.GetComponent<AudioSource>();
            }
        }

        var music = GetMusicByType(type);
        if (music != null)
        {
            _audioSource.clip = music.audioClip;
            _audioSource.Play();
        }
    }

    MusicSetup GetMusicByType(MusicType type)
    {
        return _sounds.musicSetupList.Find(item => item.musicType == type);
    }

    public SFXSetup GetSfxByType(SfxType type)
    {
        return _sounds.sfxSetupList.Find(item => item.sfxType == type);
    }
}
