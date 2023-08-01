using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : IDamageable
{
    [SerializeField] float _objLife;

    [SerializeField] GameObject _objMesh;

    [SerializeField] GameObject[] _possibleDrops;

    [SerializeField] float _minItems, _maxItems;

    void DamageOutput(int dmg, Vector3 pullFeedback)
    {
        _objLife -= dmg;

        if(_objLife <= 0)
        {
            _objMesh.SetActive(false);
        }
    }

    void DropItems()
    {
        float v = Random.Range(_minItems,_maxItems);

    }
}
