using CommonMethodsLibrary;
using System.Collections;
using UnityEngine;

public class SlimeSimple : EnemyBase
{
    Coroutine _coroutine;

    [SerializeField] float _timeBetweenBites;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        StartCoroutine(SearchingPlayer());
    }

    private void LateUpdate()
    {
        if (_player == null) return;

        transform.LookAt(_player.transform.position);

        if (PlayerOnSight())
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Attack());
            }
        }
        else
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            if (_currentDistance <= _distanceToWalk)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
                animBase.PlayAnim(Animations.AnimationType.WALK);
            }
        }
    }


    IEnumerator Attack()
    {
        while (PlayerOnSight())
        {
            animBase.PlayAnim(Animations.AnimationType.ATTACK);

            if (!PlayerOnSight())
            {
                StopCoroutine(_coroutine);
            }

            yield return new WaitForSeconds(_timeBetweenBites);
            
            animBase.PlayAnim(Animations.AnimationType.IDLE);
        }
    }
}
