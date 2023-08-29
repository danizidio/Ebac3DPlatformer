using CommonMethodsLibrary;
using System.Collections;
using UnityEngine;

public class BossBehaviour : EnemyBase
{
    [SerializeField] string[] _shaderProperties;
    float hdr = 0.0085f;

    [SerializeField] GameObject _minionSpawn;
    [SerializeField] Transform _spawnPivot;

    Coroutine _coroutine;

    int _damagetaken = 0;

    private void Start()
    {
        Init();
    }

    [NaughtyAttributes.Button]
    public void SpawnMinion()
    {
        GameObject temp = DanUtils.MakeSpawnSimpleProjectile(_minionSpawn, _spawnPivot);

        Vector3 vector3 = new Vector3(temp.transform.position.x * Random.Range(-5, 5), temp.transform.position.y * Random.Range(10, 20), temp.transform.position.z * Random.Range(-5, 5));
        temp.GetComponent<Rigidbody>().AddForce(vector3);
    }

    public override void DamageOutput(int damage, Vector3 pullFeedback)
    {
        base.DamageOutput(damage, pullFeedback);

        _damagetaken++;

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CadenceToSpawn());
        }
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
    IEnumerator CadenceToSpawn()
    {
        while (_damagetaken > 0)
        {
            yield return new WaitForSeconds(.3f);
            SpawnMinion();

            _damagetaken--;

            if (_damagetaken <= 0)
            {
                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = null;
            }

            yield return new WaitForSeconds(.6f);
        }
    }
}
