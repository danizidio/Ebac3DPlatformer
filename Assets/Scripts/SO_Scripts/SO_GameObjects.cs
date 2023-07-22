using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjects List", menuName = "Create Obj", order = 1)]
public class SO_GameObjects : ScriptableObject
{
    [SerializeField] List<GameObject> _enemies;
    public List<GameObject> enemies { get { return _enemies; } set { _enemies = value; } }

    public GameObject _boss;
    public GameObject boss { get { return _boss; } }
}
