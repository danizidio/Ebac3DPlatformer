using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PowerUpBase : CollectibleItens
{
    [SerializeField] protected ClothTypes _clothType;
    [SerializeField] protected SO_PowerUps _powerups;

    protected GameObject _player;

    protected virtual Material GetMaterial()
    {
        Material m = null;

        foreach (var item in _powerups.clothList)
        {
            if (item.type == _clothType)
            {
                m = item.material;
            }
        }

        return m;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null )
        {
            _player = player.gameObject;

            CollectedItem();
            player.GetComponent<ChangeSkin>().ChangingTexture(GetMaterial());
            Destroy(this.gameObject);
        }
    }
}
