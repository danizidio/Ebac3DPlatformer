using System;
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

        GameObject g = this.gameObject;

        CameraBehaviour.OnChangeCam?.Invoke(CamType.CHECKPOINT_CAM, g);

        _vfxPortal.enabled = true;

        GameManager.OnInstantiatePlayer?.Invoke(_vfxPortal.transform.position);

        SfxQueue.OnPlaySfx?.Invoke(SfxType.PORTAL);
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

        SfxQueue.OnPlaySfx?.Invoke(SfxType.CHECKPOINT_ACTIVATE);
    }

    private void OnEnable()
    {
        OnSpawnPlayer += SpawnPlayer;
    }
    void OnDisable()
    {
        OnSpawnPlayer -= SpawnPlayer;
    }
}
