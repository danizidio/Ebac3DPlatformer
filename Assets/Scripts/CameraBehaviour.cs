using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using NaughtyAttributes;
using CommonMethodsLibrary;
using System;

public class CameraBehaviour : MonoBehaviour
{
    public static Action OnChangeToBossCam;
    public static Action OnChangeToGamePlayCam;

    [SerializeField] CinemachineVirtualCamera[] _cameras;
    [SerializeField] CinemachineFreeLook _freeLook;

    GameObject _player;

    [Button]
    public void BossCam()
    {
        _cameras[0].Priority = 1;
        _freeLook.Priority = 0;
        //_cameras[1].Priority = 0;
    }
    [Button]
    public void GameplayCam()
    {
        _cameras[0].Priority = 0;
        _freeLook.Priority = 1;
        //_cameras[1].Priority = 1;
    }

    private void OnEnable()
    {
        OnChangeToBossCam = BossCam;
        OnChangeToGamePlayCam = GameplayCam;
    }
}
