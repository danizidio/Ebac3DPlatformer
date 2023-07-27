using CommonMethodsLibrary;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static Action OnEnemyKilled;

    public static Action<Vector3> OnInstantiatePlayer;

    [SerializeField] SO_GameObjects _enemies;
    public SO_GameObjects enemies {get{return _enemies; }}

    [SerializeField] GameObject _playerPrefab;
    Player _currentPlayer;
    public Player currentPlayer { get { return _currentPlayer; }}

    public static Func<Color> OnChangeColor;
    public static Func<Player> OnFindPlayer;

    public static Action OnCallObj;

    [SerializeField] List<Color> _colorList;
    public List<Color> colorList { get { return _colorList; } }

    [SerializeField] float _timeBetweenEnemieSpawn;
    [SerializeField] bool _canSpawnEnemies;

    [SerializeField] int _enemiesUntilBoss;
    [SerializeField] int _currentEnemiesKilled;
    
    public bool CanSpawnEnemies(bool b)
    {
        return _canSpawnEnemies = b;
    }

    public Color ChangeColor()
    {
        return DanUtils.MakeRandomItemList(_colorList);
    }

    public void RestartScene()
    {
        Destroy(_currentPlayer.gameObject);

        GameplaySateMachine.OnGameStateChange?.Invoke(StateMachines.GameStates.INITIALIZING, this.gameObject.GetComponent<GameManager>());
    }

    public void AddRandomColorToList()
    {
        Color c = new Color32((byte)UnityEngine.Random.Range(0, 255),
            (byte)UnityEngine.Random.Range(0, 255),
            (byte)UnityEngine.Random.Range(0, 255), 255);

        _colorList.Add(c);
    }

    public GameObject SpawnBoss()
    {
        GameObject t = GameObject.FindGameObjectWithTag("BOSS_SPAWN");

        GameObject g = _enemies.boss;

        GameObject temp = Instantiate(g, t.transform.position, Quaternion.identity);
        DanUtils.MakeScaleAnimation(temp.transform, .5f);

        return g;
    }

    public void InstantiateObjects()
    {
        try
        {
            GameObject[] t = GameObject.FindGameObjectsWithTag("SPAWN");

            GameObject g = DanUtils.MakeRandomItemList(_enemies.enemies);

            GameObject spawnPos = DanUtils.MakeRandomItemArray(t);

            GameObject temp = Instantiate(g, spawnPos.transform.position, Quaternion.identity);
            DanUtils.MakeScaleAnimation(temp.transform, .5f);

           // spawnPos.SetActive(false);
        }
        catch
        {
            GameObject g = DanUtils.MakeRandomItemList(_enemies.enemies);
            GameObject temp = Instantiate(g, Vector3.zero, Quaternion.identity);
            DanUtils.MakeScaleAnimation(temp.transform, .5f);
        }  
    }

    public void ChangeEnemyColor()
    {
        GameObject[] t = GameObject.FindGameObjectsWithTag("ENEMY");

        if (_colorList.Count == 0)
        {
            print("Adicionar cores na lista");
            return;
        }

        if (t.Length == 0)
        {
            print("Não há inimigos para alterar a cor");
            return;
        }

        DanUtils.MakeRandomItemArray(t).GetComponent<MeshRenderer>().material.color = ChangeColor();
    }

    Player FindingPlayer()
    {
        if(_currentPlayer == null)
        {
            Checkpoint.OnSpawnPlayer();
        }

        return _currentPlayer;
        //Player temp = null;

        //Checkpoint.OnSpawnPlayer?.Invoke();

        //if(_currentPlayer != null) temp = _currentPlayer.GetComponent<Player>();

        //return temp;

        //try
        //{
        //    print("TRIED");

        //    _currentPlayer = GameObject.FindGameObjectWithTag("Player");

        //    return _currentPlayer.GetComponent<Player>();
        //}
        //catch
        //{
        //    print("CATCHING");

        //    Checkpoint.OnSpawnPlayer?.Invoke();

        //    if(_currentPlayer != null) temp = _currentPlayer.GetComponent<Player>();

        //    return temp;
        //}
    }

    public IEnumerator RoutineSpawnEnemies()
    {
        while (_canSpawnEnemies)
        {
            InstantiateObjects();

            if(!_canSpawnEnemies)
            {
                StopCoroutine(RoutineSpawnEnemies());
            }

            yield return new WaitForSeconds(_timeBetweenEnemieSpawn);
        }
    }

    void KilledEnemies()
    {
        _currentEnemiesKilled++;

        if(_currentEnemiesKilled >= _enemiesUntilBoss)
        {
            GameplaySateMachine.OnGameStateChange?.Invoke(StateMachines.GameStates.BOSS_BATTLE, this.GetComponent<GameManager>());
        }
    }

     void SpawnPlayer(Vector3 spawnPos)
    {
        GameObject temp = Instantiate(_playerPrefab, spawnPos, Quaternion.identity);

        _currentPlayer = temp.GetComponent<Player>();  
    }

    private void OnEnable()
    {
        OnChangeColor = ChangeColor;
        OnCallObj = InstantiateObjects;
        OnFindPlayer = FindingPlayer;
        OnEnemyKilled = KilledEnemies;
        OnInstantiatePlayer = SpawnPlayer;
    }
}
