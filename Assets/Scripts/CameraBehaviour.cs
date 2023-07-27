using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using NaughtyAttributes;
using CommonMethodsLibrary;
using System;

public class CameraBehaviour : MonoBehaviour
{
    public static Action<GameObject[]> OnChangeToBossCam;
    public static Action<GameObject> OnChangeToGamePlayCam;

    [SerializeField] CinemachineVirtualCamera[] _cameras;
    [SerializeField] CinemachineFreeLook _freeLook;

    [SerializeField] CinemachineTargetGroup _targetGroup;

    GameObject _player;

    [Button]
    public void BossCam(GameObject[] groupToFocus)
    {
        foreach(var objFocus in groupToFocus)
        {
            _targetGroup.AddMember(objFocus.transform,1,1);
        }

        _cameras[0].Priority = 1;
        _freeLook.Priority = 0;
        //_cameras[1].Priority = 0;
    }
    [Button]
    public void GameplayCam(GameObject focusObj)
    {
        _freeLook.Follow = focusObj.transform;
        _freeLook.LookAt = focusObj.transform;

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
