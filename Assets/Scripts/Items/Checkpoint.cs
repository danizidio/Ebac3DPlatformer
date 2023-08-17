using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public enum Checkpoints
{
    POINT_A,
    POINT_B,
    POINT_C,
    POINT_D
}

public class Checkpoint : MonoBehaviour, Iinteractible
{
    public static Action OnSpawnPlayer;

    [SerializeField] Checkpoints _checkpoint;

    [SerializeField] List<VisualEffect> _vfxParticles;

    [SerializeField] VisualEffect _vfxPortal;

    [SerializeField] bool _enabled;

    [SerializeField] GameObject _chkpntCam;
    public GameObject chkpntCam { get { return _chkpntCam; } }


    private void Start()
    {
        SetParticlesEnable( false);
        _vfxPortal.enabled = false;
    }

    void SetParticlesEnable(bool b)
    {
        foreach (var effect in _vfxParticles)
        {
            effect.enabled = b;
        }

        //_vfxPortal.enabled = b;
    }

    public void OnInteract()
    {
        foreach (var effect in _vfxParticles)
        {
            effect.enabled = true;
        }

        SetActiveCheckPoint();
    }

    public bool IsActive(bool active)
    {
        SetParticlesEnable(active);

        return _enabled = active;
    }

    void SpawnPlayer()
    {
        if (_checkpoint != SaveManager.Instance.saveGame.checkpoints) return;

        CameraBehaviour.OnChangeCam?.Invoke(CamType.CHECKPOINT_CAM, this.gameObject);

        _vfxPortal.enabled = true;

        GameManager.OnInstantiatePlayer?.Invoke(_vfxPortal.transform.position);
    }

    void SetActiveCheckPoint()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

        foreach (var checkpoint in checkpoints) 
        {
            if(checkpoint == this)
            {
                checkpoint.IsActive(true);
            }
            else
            {
                checkpoint.IsActive(false);
            }
        }

        SaveManager.Instance.SetCheckPoint(_checkpoint);

        MessageLog.OnCallMessage?.Invoke("CHECKPOINT");
    }

    private void OnEnable()
    {
        OnSpawnPlayer += SpawnPlayer;
    }
}
