using UnityEngine;

public class Coins : CollectibleItens
{
    [SerializeField] bool _coinTaken;
    Vector3 _playerPos;

    private void OnTriggerEnter(Collider other)
    {
        p = other.GetComponentInParent<Player>();
        _playerPos = other.transform.position;

        if (p == null) return;

        CollectedItem();
    }
    private void FixedUpdate()
    {
        if(_coinTaken)
        {
            transform.position = Vector3.Lerp(transform.position, _playerPos, 5 * Time.deltaTime);
        }
    }

    protected override void CollectedItem()
    {
        base.CollectedItem();

        _coinTaken = true;

        Destroy(gameObject, 1);
    }
}
