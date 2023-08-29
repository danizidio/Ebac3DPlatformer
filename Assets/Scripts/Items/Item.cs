using UnityEngine;
using UnityEngine.VFX;

public class Item : CollectibleItens
{
    [SerializeField] bool _coinTaken;
    Vector3 _playerPos;

    [SerializeField] GameObject _coinTakeFX;

    Player _p;

    private void FixedUpdate()
    {
        if(_coinTaken)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPos, 5 * Time.deltaTime);
        }
    }
    protected override void CollectedItem()
    {
        _coinTakeFX.GetComponent<VisualEffect>().SetBool("Taken", true);

        SfxQueue.OnPlaySfx?.Invoke(sfxType);

        Inventory.OnCollectItem?.Invoke(type);

        _coinTaken = true;

        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        _p = other.GetComponentInParent<Player>();
        _playerPos = other.transform.position;

        if (_p == null) return;

        CollectedItem();
    }

}
