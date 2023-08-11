using UnityEngine;

public class SpeedPowerUp : PowerUpBase
{
    [SerializeField] float _extraSpeed;

    protected override void CollectedItem()
    {
        _player.GetComponent<PlayerBuffs>().GettingSpeedBonus();
    }
}
