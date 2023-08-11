
public class DefensePowerUP : PowerUpBase
{
    protected override void CollectedItem()
    {
        _player.GetComponent<PlayerBuffs>().GettingDefense();
    }
}

