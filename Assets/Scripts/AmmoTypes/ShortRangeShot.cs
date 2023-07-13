using CommonMethodsLibrary;
using UnityEngine;

public class ShortRangeShot : AmmoBase
{
    [Space(10)]
    [SerializeField] int _ammountPerShot;
    [SerializeField] float _angle;

    [SerializeField] GameObject _trail;

    //private void Awake()
    //{
    //    DetacheParticlePlusDestroy();
    //}

    protected override void Shot(GameObject g, Transform t)
    {
        int mult = 0;

        for (int i = 0; i < _ammountPerShot; i++)
        {
            if (i % 2 == 0)
            {
                mult++;
            }

            GameObject p = DanUtils.MakeSpawnSimpleProjectile(g, t, true, t);

            p.transform.localPosition = Vector3.zero;
            p.transform.rotation = t.rotation;
            p.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? _angle : -_angle) * mult;
            p.transform.parent = null;
        }
    }

    void DetacheParticlePlusDestroy()
    {
        _trail.transform.parent = null;
        Destroy(this.gameObject, _timeToDestroy);
    }
}
