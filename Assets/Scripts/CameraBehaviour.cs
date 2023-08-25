using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using Cinemachine;
using CommonMethodsLibrary;
using NaughtyAttributes;

public enum CamType
{
   GAMEPLAY_CAM,
   CHECKPOINT_CAM,
   VICTORY_CAM,
   BOSS_CAM
}

public class CameraBehaviour : MonoBehaviour
{
    public static Action<GameObject[]> OnChangeToBossCam;
    public static Action<float,float,float> OnShakeCam;

    public static Action<CamType, GameObject> OnChangeCam;

    [SerializeField] CinemachineVirtualCamera[] _cameras;
    [SerializeField] CinemachineFreeLook _freeLook;
    [SerializeField] CinemachineVirtualCamera _chkpntCam;

    [SerializeField] CinemachineTargetGroup _targetGroup;

    [SerializeField] CinemachineBasicMultiChannelPerlin _currentCam;
    [SerializeField] List<CinemachineBasicMultiChannelPerlin> _currentCams;

    CamType _camType;
    public CamType camType { get { return _camType; } } 

    GameObject _player;


    public void ShakeCamera(float amplitude, float frequency, float time = 0)
    {
        if (time == 0)
        {
            if (_currentCams != null)
            {
                foreach (var camShake in _currentCams)
                {
                    camShake.m_AmplitudeGain = amplitude;
                    camShake.m_FrequencyGain = frequency;
                }
            }

            if (_currentCam != null)
            {
                _currentCam.m_AmplitudeGain = amplitude;
                _currentCam.m_FrequencyGain = frequency;
            }

        }
        else
        {
            StartCoroutine(ShakeCam(amplitude, frequency, time));
        }
    }

    void ChangeCam(CamType camType, GameObject objFocus)
    {
        switch (camType)
        {
            case CamType.GAMEPLAY_CAM:
                {
                    StartCoroutine(CamTransition(objFocus));

                    break;
                }
            case CamType.CHECKPOINT_CAM:
                {
                    if(_chkpntCam != null)
                    {
                        _chkpntCam = null;
                    }

                    var temp = objFocus.GetComponent<Checkpoint>();

                    _chkpntCam = temp.chkpntCam.GetComponent<CinemachineVirtualCamera>();

                    //_freeLook.enabled = false;

                    _chkpntCam.LookAt = objFocus.transform;
                    _chkpntCam.Follow = objFocus.transform;

                    _cameras[0].Priority = 0;
                    _cameras[2].Priority = 0;
                    _freeLook.Priority = 0;
                    _chkpntCam.Priority = 1;

                    break;
                }
            case CamType.VICTORY_CAM:
                {
                    _cameras[2].Priority = 1;
                    _cameras[2].LookAt = objFocus.transform;

                    _cameras[0].Priority = 0;
                    _cameras[1].Priority = 0;
                    _freeLook.Priority = 0;

                    break;
                }
        }
    }

    IEnumerator CamTransition(GameObject objFocus)
    {
        _cameras[1].Follow = objFocus.transform; 
        _cameras[1].LookAt = objFocus.transform;
        _cameras[1].Priority = 1;
        _cameras[0].Priority = 0;
        _cameras[2].Priority = 0;
        _freeLook.Priority = 0;
        _chkpntCam.Priority = 0;

        yield return new WaitForSeconds(2);

        _freeLook.enabled = true;

        _freeLook.Follow = objFocus.transform;
        _freeLook.LookAt = objFocus.transform;

        _cameras[0].Priority = 0;
        _cameras[2].Priority = 0;
        _freeLook.Priority = 1;
        _chkpntCam.Priority = 0;

        //_currentCam = _freeLook.GetComponent<CinemachineBasicMultiChannelPerlin>();

        for (int i = 0; i < 3; i++)
        {
            _currentCams.Add(_freeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());
        }

        StopCoroutine(CamTransition(null));
    }

    public void BossCam(GameObject[] groupToFocus)
    {
        foreach(var objFocus in groupToFocus)
        {
            _targetGroup.AddMember(objFocus.transform,1,1);
        }

        _cameras[0].Priority = 1;
        _freeLook.Priority = 0;
        _chkpntCam.Priority = 0;

        _currentCam = _cameras[0].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();   

    }
    
    IEnumerator ShakeCam(float amplitude, float frequency, float time)
    {
        float t = 0;

        if (_currentCams != null)
        {
            foreach (var camShake in _currentCams)
            {
                camShake.m_AmplitudeGain = amplitude;
                camShake.m_FrequencyGain = frequency;
            }
        }

        if (_currentCam != null)
        {
            _currentCam.m_AmplitudeGain = amplitude;
            _currentCam.m_FrequencyGain = frequency;
        }

        while (t > time)
        {
            t += Time.deltaTime;

            if(t > time)
            {
                if (_currentCams != null)
                {
                    foreach (var camShake in _currentCams)
                    {
                        camShake.m_AmplitudeGain = 0;
                        camShake.m_FrequencyGain = 0;
                    }
                }

                if (_currentCam != null)
                {
                    _currentCam.m_AmplitudeGain = 0;
                    _currentCam.m_FrequencyGain = 0;
                }

                StopCoroutine(ShakeCam(0,0,0));

            }
                yield return new WaitForEndOfFrame();
        }
    }


    private void OnEnable()
    {
        OnChangeToBossCam = BossCam;
        OnShakeCam = ShakeCamera;
        OnChangeCam += ChangeCam;
    }
    private void OnDisable()
    {
        OnChangeToBossCam -= BossCam;
        OnShakeCam -= ShakeCamera;
        OnChangeCam -= ChangeCam;
    }
}
