using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] MusicType _type;
    [SerializeField] AudioSource _source;

    private void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        SoundManager.Instance.PlayMusicByType(_type);
    }
}
