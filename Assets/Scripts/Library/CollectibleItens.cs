using UnityEngine;
using UnityEngine.VFX;

public class CollectibleItens : MonoBehaviour
{
    [SerializeField] protected CollectibleTypes _type;
    public CollectibleTypes type { get { return _type; } }

    [SerializeField] SfxType _sfxType;
    public SfxType sfxType { get { return _sfxType; } }

    protected virtual void CollectedItem()
    {

    }
}
