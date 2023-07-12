using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBase : MonoBehaviour
{
    [SerializeField] float _timeToDestroy;
    [SerializeField] float _speed;
    [SerializeField] int _damage;

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
