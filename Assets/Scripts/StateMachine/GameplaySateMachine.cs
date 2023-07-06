using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

public class GameplaySateMachine : MonoBehaviour
{
    public StateMachine<GameStates> statemachine;
    void Start()
    {
        //statemachine = new StateMachine<GameStates>(GameStates.INITIALIZING);
        statemachine = new StateMachine<GameStates>();

        statemachine.StartStateMachine();

        statemachine.RegisterStates(GameStates.INITIALIZING, new GameplayState_INITIALIZING());
        statemachine.RegisterStates(GameStates.START, new GameplayState_START());
        statemachine.RegisterStates(GameStates.GAMEPLAY, new GameplayState_GAMEPLAY());
        statemachine.RegisterStates(GameStates.PAUSE, new StateBase());
        statemachine.RegisterStates(GameStates.GAMEOVER, new StateBase());

        statemachine.SwitchState(GameStates.INITIALIZING);
    }
}
