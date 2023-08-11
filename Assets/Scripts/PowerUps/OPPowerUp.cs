
public class OPPowerUp : PowerUpBase
{

    protected override void CollectedItem()
    {
        _player.GetComponent<PlayerBuffs>().GettingStronger();
    }
}
