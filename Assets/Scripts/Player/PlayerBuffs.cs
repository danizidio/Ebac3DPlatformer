using CommonMethodsLibrary;
using System.Collections;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    Player _player;

    [SerializeField] float _maxBonusTime;
    public float maxBonusTime { get { return _maxBonusTime; } }

    [Header("SPEED")]
    [SerializeField] float _speedBoostValue;

    [Header("ATTACK")]
    [SerializeField] float _opTime;
    [SerializeField] SpreadShot _projectile;
    [SerializeField] GameObject _burningGround;
    [SerializeField] Transform _shootPivot;
    [SerializeField] float _shootTime;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void GettingSpeedBonus()
    {
        StartCoroutine(SpeedBonus());
    }

    public void GettingStronger()
    {
        StartCoroutine(PowerBonus());
    }

    public void GettingDefense()
    {
        StartCoroutine(PowerDefense());
    }

    IEnumerator SpeedBonus()
    {
        _player.bonusSpeed = _speedBoostValue;

        yield return new  WaitForSeconds(_maxBonusTime);

        _player.bonusSpeed = 0;

        _player.GetComponent<ChangeSkin>().ResetMaterial();

        StopCoroutine(SpeedBonus());
    }

    public void PowerShoot()
    {
        int mult = 0;

        for (int i = 0; i < _projectile.ammountPerShot; i++)
        {
            if (i % 2 == 0)
            {
                mult++;
            }

            GameObject p =DanUtils.MakeSpawnSimpleProjectile(_projectile.gameObject, _shootPivot, true, _shootPivot); 

            p.transform.localPosition = Vector3.zero;
            p.transform.rotation = _player.transform.rotation;
            p.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? _projectile.angle : -_projectile.angle) * mult;
            p.transform.parent = null;
        }
    }
    IEnumerator PowerBonus()
    {
        float t = 0;

        while (t <= _opTime)
        {
            _burningGround.SetActive(true);

            t += Time.deltaTime;

            PowerShoot();

            if(t >= _opTime)
            {
                _burningGround.SetActive(false);

                _player.GetComponent<ChangeSkin>().ResetMaterial();

                StopCoroutine(PowerBonus());
            }

            yield return new WaitForSeconds(_shootTime);
        }
    }

    IEnumerator PowerDefense()
    {
        yield return new WaitForSeconds(_maxBonusTime);

        _player.GetComponent<ChangeSkin>().ResetMaterial();

        StopCoroutine(PowerDefense());
    }
}
