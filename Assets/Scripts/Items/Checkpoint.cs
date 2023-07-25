using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using NaughtyAttributes;

public class Checkpoint : MonoBehaviour, Iinteractible
{
    public static Action OnSpawnPlayer;

    [SerializeField] List<VisualEffect> _vfxParticles;

    [SerializeField] VisualEffect _vfxPortal;

    [SerializeField] bool _enabled;

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
        if (!_enabled) return;

        GameManager gm = FindAnyObjectByType<GameManager>();

        if (gm != null)
        {
            if (gm.currentPlayer == null)
            {
                _vfxPortal.enabled = true;
                gm.SpawnPlayer(_vfxPortal.transform.position);
            }
            else
            {
                _vfxPortal.enabled = false;
            }
        }
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

    }

    private void OnEnable()
    {
        OnSpawnPlayer = SpawnPlayer;
    }
}
