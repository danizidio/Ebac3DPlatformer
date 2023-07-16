using StateMachines;
using System;

public class GameplayState_INITIALIZING : StateBase
{
    public override void OnStateEnter(object o = null)
    {        

    }
    public override void OnStateStay()
    {
        if (GameManager.OnFindPlayer?.Invoke() != null)
        {
            GameplaySateMachine.OnGameStateChange?.Invoke(GameStates.START, null);
        }
    }
}
