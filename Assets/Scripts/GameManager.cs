using CommonMethodsLibrary;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static Action OnEnemyKilled;

    [SerializeField] SO_GameObjects _enemies;
    public SO_GameObjects enemies {get{return _enemies; }}

    [SerializeField] GameObject _playerPrefab;
    GameObject _currentPlayer;
    public GameObject currentPlayer { get { return _currentPlayer; }}

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

    public void AddRandomColorToList()
    {
        Color c = new Color32((byte)UnityEngine.Random.Range(0, 255),
            (byte)UnityEngine.Random.Range(0, 255),
            (byte)UnityEngine.Random.Range(0, 255), 255);

        _colorList.Add(c);
    }

    public void SpawnBoss()
    {
        GameObject t = GameObject.FindGameObjectWithTag("BOSS_SPAWN");

        GameObject g = _enemies.boss;

        GameObject temp = Instantiate(g, t.transform.position, Quaternion.identity);
        DanUtils.MakeScaleAnimation(temp.transform, .5f);
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
        Player temp = null;

        try
        {
            return _currentPlayer.GetComponent<Player>();
        }
        catch
        {
            Checkpoint.OnSpawnPlayer?.Invoke();

            return temp = _currentPlayer.GetComponent<Player>();
        }
        finally
        {
            _currentPlayer = GameObject.FindGameObjectWithTag("Player");
        }
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

    public void SpawnPlayer(Vector3 spawnPos)
    {
        _currentPlayer = Instantiate(_playerPrefab, spawnPos, Quaternion.identity);
    }

    private void OnEnable()
    {
        OnChangeColor = ChangeColor;
        OnCallObj = InstantiateObjects;
        OnFindPlayer = FindingPlayer;
        OnEnemyKilled = KilledEnemies;
    }
}
