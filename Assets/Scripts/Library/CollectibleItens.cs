using UnityEngine;
using UnityEngine.VFX;

public enum CollectibleTypes
{
    COLLECTIBLE_COIN
   ,COLLECTIBLE_HEALTHPACK
   ,COLLECTIBLE_CHEST
}

public class CollectibleItens : MonoBehaviour
{
    [SerializeField] protected CollectibleTypes _type;
    public CollectibleTypes type { get { return _type; } }



    protected virtual void CollectedItem()
    {

    }
}
