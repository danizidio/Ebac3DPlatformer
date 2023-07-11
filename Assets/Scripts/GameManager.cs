using CommonMethodsLibrary;
using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [SerializeField] SO_GameObjects _gameObjects;

    public static Func<Color> OnChangeColor;

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

    [Button]
    public void InstantiateObjects()
    {
        try
        {
            GameObject[] t = GameObject.FindGameObjectsWithTag("SPAWN");

            GameObject g = DanUtils.MakeRandomItemList(_gameObjects.gameObjects);
            GameObject temp = Instantiate(g, DanUtils.MakeRandomItemArray(t).transform.position, Quaternion.identity);
            DanUtils.MakeScaleAnimation(temp.transform, 1, .5f);
        }
        catch
        {
            GameObject g = DanUtils.MakeRandomItemList(_gameObjects.gameObjects);
            GameObject temp = Instantiate(g, Vector3.zero, Quaternion.identity);
            DanUtils.MakeScaleAnimation(temp.transform, 1, .5f);
        }  
    }

    [Button]
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


    private void OnEnable()
    {
        OnChangeColor = ChangeColor;
        OnCallObj = InstantiateObjects;
    }
}
