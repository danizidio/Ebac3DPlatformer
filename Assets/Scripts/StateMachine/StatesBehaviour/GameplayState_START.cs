using StateMachines;
using UnityEngine;

public class GameplayState_START : StateBase
{
    GameManager g;

    public override void OnStateEnter(object o = null)
    {
        g = (GameManager)o;
        //g.CanSpawnEnemies(true);
    }
    public override void OnStateStay()
    {
        GameplaySateMachine.OnGameStateChange?.Invoke(GameStates.GAMEPLAY, g);
    }
}
