using UnityEngine;
using UnityEngine.VFX;

public class CollectibleItens : MonoBehaviour
{
    [SerializeField] protected CollectibleTypes _type;
    public CollectibleTypes type { get { return _type; } }

    protected virtual void CollectedItem()
    {

    }
}
