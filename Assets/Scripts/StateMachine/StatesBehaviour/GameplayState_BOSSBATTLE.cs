using StateMachines;
using UnityEngine;

public class GameplayState_BOSSBATTLE : StateBase
{
    GameManager g;
    
    public override void OnStateEnter(object o = null)
    {
        g = (GameManager)o;

        g.CanSpawnEnemies(false);

        g.SpawnBoss();

        CameraBehaviour.OnChangeToBossCam?.Invoke();

        Debug.Log("Entrou na Boss Battle");
    }

    public override void OnStateStay()
    {

    }

    public override void OnStateExit()
    {
        Debug.Log("Saiu da Boss Battle");
    }
}
