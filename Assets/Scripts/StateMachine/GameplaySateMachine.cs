using UnityEngine;
using StateMachines;
using System;
using DG.Tweening.Core.Easing;

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
        statemachine.RegisterStates(GameStates.BOSS_BATTLE, new GameplayState_BOSSBATTLE());
        statemachine.RegisterStates(GameStates.PAUSE, new GameplayState_PAUSE());
        statemachine.RegisterStates(GameStates.GAMEOVER, new GameplayState_GAMEOVER());

        OnGameStateChange = statemachine.SwitchState;
    }

    private void Start()
    {
        OnGameStateChange?.Invoke(GameStates.INITIALIZING, this.gameObject.GetComponent<GameManager>());
    }

    void Update()
    {
        statemachine.Update();
    }
}

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
        Player temp = GameManager.OnFindPlayer?.Invoke();

        if (temp != null)
        {
            GameplaySateMachine.OnGameStateChange?.Invoke(GameStates.START, g);
        }
    }
}

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

public class GameplayState_GAMEPLAY : StateBase
{
    GameManager g;

    public override void OnStateEnter(object o = null)
    {
        g = (GameManager)o;

        g.StartCoroutine(g.RoutineSpawnEnemies());

        EnemyBase.OnSearchingPlayer?.Invoke();

        CameraBehaviour.OnChangeCam?.Invoke(CamType.GAMEPLAY_CAM, g.currentPlayer.gameObject);

        //Debug.Log("Entrou do Gameplay");
    }

    public override void OnStateStay()
    {

    }

    public override void OnStateExit()
    {
        //Debug.Log("Saiu do Gameplay");
    }

}

public class GameplayState_BOSSBATTLE : StateBase
{
    GameManager g;

    public override void OnStateEnter(object o = null)
    {
        g = (GameManager)o;

        g.CanSpawnEnemies(false);

        GameObject temp = g.SpawnBoss();

        GameObject[] gameObjects = { temp, g.currentPlayer.gameObject };

        CameraBehaviour.OnChangeToBossCam?.Invoke(gameObjects);

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

public class GameplayState_PAUSE : StateBase
{
    GameManager g;

    public override void OnStateEnter(object o = null)
    {
        g = (GameManager)o;


    }

    public override void OnStateStay()
    {

    }

    public override void OnStateExit()
    {
        Debug.Log("Saiu do Pause");
    }
}

public class GameplayState_GAMEOVER : StateBase
{
    GameManager g;

    public override void OnStateEnter(object o = null)
    {
        g = (GameManager)o;

        Debug.Log("Morreu");

        g.RestartScene();
    }

    public override void OnStateStay()
    {

    }

    public override void OnStateExit()
    {

    }
}
