using UnityEngine;
using UnityEngine.VFX;

public enum CollectibleTypes
{
    COLLECTIBLE_COIN
    ,COLLECTIBLE_HEALTHPACK
}

public class CollectibleItens : MonoBehaviour
{
    [SerializeField] protected CollectibleTypes _type;
    public CollectibleTypes type { get { return _type; } }

    [SerializeField] GameObject _coinTakeFX;

    protected Player p;

    protected virtual void CollectedItem()
    {
        //Instantiate(_coinTakeFX, this.transform.position, Quaternion.identity);
        _coinTakeFX.GetComponent<VisualEffect>().SetBool("Taken", true);
        Inventory.OnCollectItem?.Invoke(type);
    }
}
