using UnityEngine;
using UnityEngine.VFX;

public class Coins : CollectibleItens
{
    [SerializeField] bool _coinTaken;
    Vector3 _playerPos;

    [SerializeField] GameObject _coinTakeFX;

    Player _p;

    private void FixedUpdate()
    {
        if(_coinTaken)
        {
            transform.position = Vector3.Lerp(transform.position, _playerPos, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _p = other.GetComponentInParent<Player>();
        _playerPos = other.transform.position;

        if (_p == null) return;

        CollectedItem();
    }

    protected override void CollectedItem()
    {
        _coinTakeFX.GetComponent<VisualEffect>().SetBool("Taken", true);

        Inventory.OnCollectItem?.Invoke(type);

        _coinTaken = true;

        Destroy(gameObject, 1);
    }
}
