using CommonMethodsLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Action<CollectibleTypes> OnCollectItem;
    public delegate bool onVerifyStock(CollectibleTypes t);
    public static onVerifyStock OnVerifyStock;

    [SerializeField] List<Collectibles> _collectibles;

    private void Start()
    {
        UpdateInterfaceValues();
    }

    void UpdateInterfaceValues()
    {
        foreach (var col in _collectibles)
        {
            col.text.text = col.collectible.count.ToString();
        }
    }

    void CollectItem(CollectibleTypes item)
    {
        foreach (var col in _collectibles)
        {
            if (col.collectible.type == item)
            {
                col.collectible.count++;

                col.text.text = col.collectible.count.ToString();
            }
        }
    }

    bool CheckItem(CollectibleTypes item)
    {
        foreach (var col in _collectibles)
        {
            if (col.collectible.type == item)
            {
                if (col.collectible.count > 0)
                {
                    col.collectible.count--;

                    UpdateInterfaceValues();

                    return true;
                }
            }
        }
        return false;
    }

    private void OnEnable()
    {
        OnCollectItem = CollectItem;
        OnVerifyStock = CheckItem;
    }
}

[Serializable]
public class Collectibles
{
    [SerializeField] SO_Collectibles _collectible;
    public SO_Collectibles collectible { get { return _collectible; } set { _collectible = value; } }

    [SerializeField] TMP_Text _text;
    public TMP_Text text { get { return _text; } set { _text = value; } }
}
