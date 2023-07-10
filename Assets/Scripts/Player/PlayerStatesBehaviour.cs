using UnityEngine;
using StateMachines;

public class PlayerStatesBehaviour : MonoBehaviour
{
    public StateMachine<PlayerStates> statemachine;

    public static Player _p;
    public Player p { get { return _p; } }

    private void Awake()
    {
        _p = GetComponent<Player>();    
    }

    void Start()
    {
        statemachine = new StateMachine<PlayerStates>();

        statemachine.StartStateMachine();

        statemachine.RegisterStates(PlayerStates.IDLE, new Player_IdleBehaviour());
        statemachine.RegisterStates(PlayerStates.WALKING, new Player_WalkingBehaviour());
        statemachine.RegisterStates(PlayerStates.JUMPING, new Player_JumpingBehaviour());
        statemachine.RegisterStates(PlayerStates.ATTACKING, new Player_AttackingBehaviour());
        statemachine.RegisterStates(PlayerStates.TAKING_DAMAGE, new Player_TakingDamageBehaviour());
        statemachine.RegisterStates(PlayerStates.DEAD, new Player_DeadBehaviour());

        statemachine.SwitchState(PlayerStates.IDLE);
    }
}

public class Player_IdleBehaviour : StateBase
{

}
public class Player_WalkingBehaviour : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        PlayerStatesBehaviour._p.GetComponent<Player>().anim.SetBool("WALK", true);
    }
    public override void OnStateStay()
    {

    }

    public override void OnStateExit()
    {
        PlayerStatesBehaviour._p.GetComponent<Player>().anim.SetBool("WALK", false);
    }
}
public class Player_JumpingBehaviour : StateBase
{

}
public class Player_AttackingBehaviour : StateBase
{

}
public class Player_TakingDamageBehaviour : StateBase
{

}
public class Player_DeadBehaviour : StateBase
{

}