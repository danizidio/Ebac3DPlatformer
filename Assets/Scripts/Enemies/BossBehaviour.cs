using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBase
{
    [SerializeField] string[] _shaderProperties;
    float hdr = 0.0085f;

    [SerializeField] GameObject _minionSpawn;
    [SerializeField] Transform _spawnPivot;

    private void Start()
    {
        Init();
    }

    protected override void SetColors(Color color, string shaderProperty = null)
    {
        base.SetColors(color, shaderProperty);

        float a = 1;
        foreach (var property in _shaderProperties) 
        {
            hdr *= a;
            _mr.material.SetColor(property, new Color(_predominantColor.r * hdr, _predominantColor.g * hdr, _predominantColor.b * hdr, 255));
            a++;
        }
    }
}
