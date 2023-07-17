using StateMachines;
using System;

public class GameplayState_INITIALIZING : StateBase
{
    GameManager g;
    public override void OnStateEnter(object o = null)
    {        
        g = (GameManager)o;

        g.CanSpawnEnemies(false);
    }
    public override void OnStateStay()
    {
        if (GameManager.OnFindPlayer?.Invoke() != null)
        {
            GameplaySateMachine.OnGameStateChange?.Invoke(GameStates.START, g);
        }
    }
}
