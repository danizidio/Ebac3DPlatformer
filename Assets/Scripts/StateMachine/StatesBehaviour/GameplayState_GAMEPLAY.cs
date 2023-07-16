using StateMachines;
using System.Collections;
using UnityEngine;

public class GameplayState_GAMEPLAY : StateBase
{
    GameManager g;

    public override void OnStateEnter(object o = null)
    {
        g = (GameManager)o;
        
       g.StartCoroutine(g.RoutineSpawnEnemies());
        
        Debug.Log("Entrou do Gameplay");
    }

    public override void OnStateStay()
    {

    }

    public override void OnStateExit()
    {
        Debug.Log("Saiu do Gameplay");
    }

}
