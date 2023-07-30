using UnityEngine;

[CreateAssetMenu(fileName = "Collectible item", menuName = "Create New Collectible", order = 1)]
public class SO_Collectibles : ScriptableObject
{
    [SerializeField] CollectibleTypes _type;
    public CollectibleTypes type { get { return _type; } }

    [SerializeField] int _count;
    public int count { get { return _count; } set { _count = value; } } 
}
