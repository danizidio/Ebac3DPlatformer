using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxQueue : MonoBehaviour
{
    public static Action<SfxType> OnPlaySfx;
    
    [SerializeField] int _poolSize;

    [SerializeField] List<AudioSource> _audioSourceList;


    int _index;

    private void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
        AudioMixerGroup[] audioMixGroup = SoundManager.Instance.So_Sounds.mixerGroup.FindMatchingGroups("Master/SFX");

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject audio = new GameObject("Sfx_Asset");
            audio.transform.SetParent(this.gameObject.transform);
            _audioSourceList.Add(audio.AddComponent<AudioSource>());
            audio.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixGroup[0];
        }
    }

    void PlaySfx(SfxType type)
    {
        if (type == SfxType.NONE) return;

        SFXSetup sfx = SoundManager.Instance.GetSfxByType(type);

        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;

        if(_index >- _audioSourceList.Count)
        {
            _index = 0;
        }
    }

    void OnEnable()
    {
        OnPlaySfx += PlaySfx;
    }
    void OnDisable()
    {
        OnPlaySfx -= PlaySfx;
    }
}
