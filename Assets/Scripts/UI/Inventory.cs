using CommonMethodsLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Action<CollectibleTypes> OnCollectItem;
    public delegate bool onVerifyStock(CollectibleTypes t);
    public static onVerifyStock OnVerifyStock;

    public static Action OnSaveInventory;

    [SerializeField] List<Collectibles> _collectibles;

    private void Start()
    {
        LoadInventory();
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

    void LoadInventory()
    {
        foreach (Collectibles collectible in _collectibles)
        {
            if (collectible.collectible.type == CollectibleTypes.COLLECTIBLE_COIN)
            {
                collectible.collectible.count = SaveManager.Instance.saveGame.coinsTaken;
            }
            if (collectible.collectible.type == CollectibleTypes.COLLECTIBLE_HEALTHPACK)
            {
                collectible.collectible.count = SaveManager.Instance.saveGame.medPacks;
            }
        }
    }

    void SaveInventory()
    {
        foreach (Collectibles collectible in _collectibles)
        {
            if (collectible.collectible.type == CollectibleTypes.COLLECTIBLE_COIN)
            {
                SaveManager.Instance.SaveInventory(CollectibleTypes.COLLECTIBLE_COIN, collectible.collectible.count);
            }
            if (collectible.collectible.type == CollectibleTypes.COLLECTIBLE_HEALTHPACK)
            {
                SaveManager.Instance.SaveInventory(CollectibleTypes.COLLECTIBLE_HEALTHPACK, collectible.collectible.count);
            }
        }
    }

    private void OnEnable()
    {
        OnCollectItem = CollectItem;
        OnVerifyStock = CheckItem;
        OnSaveInventory = SaveInventory;
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
