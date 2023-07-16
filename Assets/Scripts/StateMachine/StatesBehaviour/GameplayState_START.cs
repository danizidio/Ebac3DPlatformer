using StateMachines;
using UnityEngine;

public class GameplayState_START : StateBase
{
    public override void OnStateEnter(object o = null)
    {

    }
    public override void OnStateStay()
    {
        GameManager g = GameObject.FindObjectOfType<GameManager>();

        GameplaySateMachine.OnGameStateChange?.Invoke(GameStates.GAMEPLAY, g);
    }
}
