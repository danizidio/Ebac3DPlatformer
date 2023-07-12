using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBase : MonoBehaviour
{
    [SerializeField] int _ammoLimit;
    public int ammoLimit { get { return _ammoLimit; } }

    [SerializeField] float _speed;
    [SerializeField] int _damage;

    [Space(10)]
    [SerializeField] float _timeToDestroy;
    [SerializeField] float _coolDownShoots;
    public float coolDownShoots { get { return _coolDownShoots; } }

    private void Awake()
    {
        Destroy(this.gameObject, _timeToDestroy);
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();
        if (enemy != null)
        {
            enemy.IDamageOutput(_damage);
            Destroy(this.gameObject);
        }
    }
}
