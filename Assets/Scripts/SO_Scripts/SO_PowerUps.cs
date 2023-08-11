using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cloth Manager", menuName = "ClothCollection", order = -1)]   
public class SO_PowerUps : ScriptableObject
{
    [SerializeField] List<ClothSetup> _clothList;
    public List<ClothSetup> clothList { get { return _clothList; } }
}

[System.Serializable]
public class ClothSetup
{
    [SerializeField] ClothTypes _type;
    public ClothTypes type { get { return _type; } }

    [SerializeField] Material _material;
    public Material material { get { return _material; } }
}

        
