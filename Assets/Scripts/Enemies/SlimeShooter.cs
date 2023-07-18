using CommonMethodsLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShooter : EnemyBase
{

    [SerializeField] Transform _shootPivot;
    [SerializeField] GameObject _projectile;
    [SerializeField] float _timeBetweenShots;

    Coroutine _coroutine;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void LateUpdate()
    {
        if (_player == null) return;

        transform.LookAt(_player.transform.position);

        if (PlayerOnSight())
        {
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(ShootProjectile());
            }
        }
        else
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
            animBase.PlayAnim(Animations.AnimationType.WALK);
        }
    }

    IEnumerator ShootProjectile()
    {
        while (PlayerOnSight())
        {
            animBase.PlayAnim(Animations.AnimationType.ATTACK);

            DanUtils.MakeSpawnSimpleProjectile(_projectile, _shootPivot);

            if(!PlayerOnSight())
            {
                StopCoroutine(_coroutine);
            }

            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }
}
