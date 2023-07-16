using UnityEngine;
using StateMachines;
using System;

public class GameplaySateMachine : MonoBehaviour
{
    public static Action<GameStates, object> OnGameStateChange;

    public StateMachine<GameStates> statemachine;

    void Awake()
    {
        statemachine = new StateMachine<GameStates>();

        statemachine.StartStateMachine();

        statemachine.RegisterStates(GameStates.INITIALIZING, new GameplayState_INITIALIZING());
        statemachine.RegisterStates(GameStates.START, new GameplayState_START());
        statemachine.RegisterStates(GameStates.GAMEPLAY, new GameplayState_GAMEPLAY());
        statemachine.RegisterStates(GameStates.PAUSE, new StateBase());
        statemachine.RegisterStates(GameStates.GAMEOVER, new StateBase());

        OnGameStateChange = statemachine.SwitchState;
    }

    private void Start()
    {
        OnGameStateChange?.Invoke(GameStates.INITIALIZING, null);
    }
    void Update()
    {
        statemachine.Update();
    }
}
