using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Checkpoint : MonoBehaviour, Iinteractible
{
    [SerializeField] List<VisualEffect> _vfx;


    private void Start()
    {
        foreach (var effect in _vfx)
        {
            effect.enabled = false;
        }
    }

    public void OnInteract()
    {
        foreach (var effect in _vfx)
        {
            effect.enabled = true;
        }
    }
}
