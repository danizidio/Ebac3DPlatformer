using Animations;
using UnityEngine;

[RequireComponent(typeof(AnimationBase))]
public class EnemyBase : MonoBehaviour, IDamageable
{
    Rigidbody _rb;

    AnimationBase _animBase;
    protected AnimationBase animBase { get { return _animBase; } }

    [SerializeField] float _maxHealth;

    [SerializeField] protected float _currentLife;

    protected float _moveSpeed;

    bool _canMove;
    public bool canMove { get { return _canMove; } }


    protected virtual void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _animBase = GetComponent<AnimationBase>();
        _animBase.SetAnim(this.GetComponentInChildren<Animator>());

        _currentLife = _maxHealth;
    }

    public void DamageOutput(int damage)
    {
        _currentLife -= damage;

        if(_currentLife <= 0)
        {
            _animBase.PlayAnim(AnimationType.DEAD);
            Destroy(this.gameObject, 3f);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        

    }

}
