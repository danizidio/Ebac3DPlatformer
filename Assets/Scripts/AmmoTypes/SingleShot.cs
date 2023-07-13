using UnityEngine;
using DG.Tweening;
using CommonMethodsLibrary;

public class SingleShot : AmmoBase
{
    [Space(10)]
    [SerializeField] GameObject _visual;
    protected override void Update()
    {
        base.Update();

         _visual.transform.DORotate(Vector3.right * 360, 1f, RotateMode.WorldAxisAdd);
    }

    protected override void Shot(GameObject g, Transform t)
    {
        DanUtils.MakeSpawnSimpleProjectile(g, t);
    }
}
