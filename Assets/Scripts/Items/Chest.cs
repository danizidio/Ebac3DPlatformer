using CommonMethodsLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Chest : CollectibleItens
{
    [SerializeField] float _minCoins, _maxCoins;

    [SerializeField] VisualEffect _coins;

    [SerializeField] GameObject _visualIndicator;

    [SerializeField] List<VisualEffect> _effects;

    Player _p;

    private void Start()
    {
        _coins.Stop();
        HideInteractibleUI();
    }

    protected override void CollectedItem()
    {
        GetComponent<Animator>().SetTrigger("OPENED");

        if (_p.OnInteracting != null)
        {
            _p.OnInteracting = null;
        }
    }


    void ShowInteractibleUI()
    {
        _visualIndicator.transform.localScale = Vector3.one;
        DanUtils.MakeScaleAnimation(_visualIndicator.transform,.3f,1);

        MessageLog.OnCallMessage("Press 'CTRL' to interact");
    }

    void HideInteractibleUI()
    {
        DanUtils.MakeScaleAnimation(_visualIndicator.transform, .6f,0,true);
    }

    public void ShowCoins()
    {
        int f = (int)Random.Range(_minCoins, _maxCoins);

        for (int i = 0; i < f; i++)
        {
            _effects.Add(Instantiate(_coins, new Vector3(this.transform.position.x, 
                                                         this.transform.position.y + 1, 
                                                         this.transform.position.z),
                                                 Quaternion.Euler(0,0,0),   
                                                 this.transform));

            Inventory.OnCollectItem?.Invoke(CollectibleTypes.COLLECTIBLE_COIN);
        }

        foreach(VisualEffect effect in _effects)
        {
            effect.Play();

            Destroy(effect.gameObject, 4);
        }

        _effects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        _p = other.GetComponent<Player>();

        if (_p == null) return;

        ShowInteractibleUI();

        _p.OnInteracting = CollectedItem;
    }

    private void OnTriggerExit(Collider other)
    {
        _p = other.GetComponent<Player>();

        if (_p == null) return;

        HideInteractibleUI();

        _p.OnInteracting = null;

        _p = null;
    }
}
