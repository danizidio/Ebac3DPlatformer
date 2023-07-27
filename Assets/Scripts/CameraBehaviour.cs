using UnityEngine;
using Cinemachine;
using CommonMethodsLibrary;
using System;
using System.Collections;

public enum CamType
{
    GAMEPLAY_CAM
   , CHECKPOINT_CAM
   , BOSS_CAM
}

public class CameraBehaviour : MonoBehaviour
{
    public static Action<GameObject[]> OnChangeToBossCam;
    //public static Action<GameObject> OnChangeToGamePlayCam;

    public static Action<CamType, GameObject> OnChangeCam;

    [SerializeField] CinemachineVirtualCamera[] _cameras;
    [SerializeField] CinemachineFreeLook _freeLook;
    [SerializeField] CinemachineVirtualCamera _chkpntCam;

    [SerializeField] CinemachineTargetGroup _targetGroup;
    
    CamType _camType;
    public CamType camType { get { return _camType; } } 

    GameObject _player;

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
                    var temp = objFocus.GetComponent<Checkpoint>();

                    _chkpntCam = temp.chkpntCam.GetComponent<CinemachineVirtualCamera>();

                    _freeLook.enabled = false;

                    _chkpntCam.LookAt = objFocus.transform;
                    _chkpntCam.Follow = objFocus.transform;

                    _cameras[0].Priority = 0;
                    _freeLook.Priority = 0;
                    _chkpntCam.Priority = 1;

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
        _freeLook.Priority = 0;
        _chkpntCam.Priority = 0;

        yield return new WaitForSeconds(2);

        _freeLook.enabled = true;

        _freeLook.Follow = objFocus.transform;
        _freeLook.LookAt = objFocus.transform;

        _cameras[0].Priority = 0;
        _freeLook.Priority = 1;
        _chkpntCam.Priority = 0;

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
    }
    


    private void OnEnable()
    {
        OnChangeToBossCam = BossCam;
        //OnChangeToGamePlayCam = GameplayCam;
        OnChangeCam += ChangeCam;
    }
}
