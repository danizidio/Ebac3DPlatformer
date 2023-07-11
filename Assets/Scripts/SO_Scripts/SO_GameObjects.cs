using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjects List", menuName = "Create Obj", order = 1)]
public class SO_GameObjects : ScriptableObject
{
    [SerializeField] List<GameObject> _gameObjects;
    public List<GameObject> gameObjects { get { return _gameObjects; } set { _gameObjects = value; } }
}
