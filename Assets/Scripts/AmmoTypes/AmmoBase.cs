using UnityEngine;

public class AmmoBase : MonoBehaviour
{
    [SerializeField] int _ammoLimit;
    public int ammoLimit { get { return _ammoLimit; } }

    [SerializeField] float _speed;
    [SerializeField] int _damage;

    [SerializeField] protected float _timeToDestroy;
    [SerializeField] float _coolDownShoots;

    [SerializeField] float _rechargeTime;
    public float rechargeTime { get { return _rechargeTime; } } 
    
    public float coolDownShoots { get { return _coolDownShoots; } }

    private void Awake()
    {
        Destroy(this.gameObject, _timeToDestroy);
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    protected virtual void Shot(GameObject g, Transform t)
    {

    }

    public void SpawnShot(GameObject g, Transform t)
    {
        Shot(g, t);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();
        if (enemy != null)
        {
            enemy.IDamageOutput(_damage);
        }
            Destroy(this.gameObject);
    }
}
