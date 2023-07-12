using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SingleShot : AmmoBase
{
    [SerializeField] GameObject _visual;
    protected override void Update()
    {
        base.Update();

         _visual.transform.DORotate(Vector3.right * 360, 1f, RotateMode.WorldAxisAdd);
    }
}
