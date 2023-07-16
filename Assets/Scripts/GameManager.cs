using CommonMethodsLibrary;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] SO_GameObjects _gameObjects;

    public static Func<Color> OnChangeColor;
    public static Func<Player> OnFindPlayer;

    public static Action OnCallObj;

    [SerializeField] List<Color> _colorList;

    public List<Color> colorList { get { return _colorList; } }

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

    public void InstantiateObjects()
    {
        try
        {
            GameObject[] t = GameObject.FindGameObjectsWithTag("SPAWN");

            GameObject g = DanUtils.MakeRandomItemList(_gameObjects.gameObjects);

            GameObject spawnPos = DanUtils.MakeRandomItemArray(t);

            GameObject temp = Instantiate(g, spawnPos.transform.position, Quaternion.identity);
            DanUtils.MakeScaleAnimation(temp.transform, .5f);

            spawnPos.SetActive(false);
        }
        catch
        {
            GameObject g = DanUtils.MakeRandomItemList(_gameObjects.gameObjects);
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
        Player temp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        return temp;
    }

    public IEnumerator RoutineSpawnEnemies()
    {
        InstantiateObjects();

        yield return new WaitForSeconds(1);

        InstantiateObjects();

        yield return new WaitForSeconds(1);

        InstantiateObjects(); 
        
        yield return new WaitForSeconds(1);

        InstantiateObjects();
    }

    private void OnEnable()
    {
        OnChangeColor = ChangeColor;
        OnCallObj = InstantiateObjects;
        OnFindPlayer = FindingPlayer;
    }
}
