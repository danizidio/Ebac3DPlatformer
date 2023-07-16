using UnityEngine;
using StateMachines;
using System;

public class PlayerStatesBehaviour : MonoBehaviour
{
    public static Action<PlayerStates, object> OnPlayerStateChange;

    public StateMachine<PlayerStates> statemachine;

    void Awake()
    {
        statemachine = new StateMachine<PlayerStates>();

        statemachine.StartStateMachine();

        statemachine.RegisterStates(PlayerStates.IDLE, new Player_IdleBehaviour());
        statemachine.RegisterStates(PlayerStates.WALKING, new Player_WalkingBehaviour());
        statemachine.RegisterStates(PlayerStates.JUMPING, new Player_JumpingBehaviour());
        statemachine.RegisterStates(PlayerStates.FALL, new Player_Fall());
        statemachine.RegisterStates(PlayerStates.ATTACKING, new Player_AttackingBehaviour());
        statemachine.RegisterStates(PlayerStates.TAKING_DAMAGE, new Player_TakingDamageBehaviour());
        statemachine.RegisterStates(PlayerStates.DEAD, new Player_DeadBehaviour());

        OnPlayerStateChange = statemachine.SwitchState;
    }

    void Update()
    {
        statemachine.Update();
    }
}

public class Player_IdleBehaviour : StateBase
{
    Player _player;
    public override void OnStateEnter(object o = null)
    {
        _player = o as Player;

        _player.animBase.PlayAnim(Animations.AnimationType.WALK, false);
        
        //Debug.Log("entrou do idle");
    }

    public override void OnStateStay()
    {
        if (_player.Moving() != Vector3.zero && _player.onGround)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.WALKING, _player);
        }

        if(_player.jumped)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.JUMPING, _player);
        }
    }

    public override void OnStateExit()
    {
        //Debug.Log("saiu do idle");
    }

}

public class Player_WalkingBehaviour : StateBase
{
    Player _player;
    public override void OnStateEnter(object o = null)
    {
        _player = (Player)o;
        _player.animBase.PlayAnim(Animations.AnimationType.WALK, true);
        //Debug.Log("entrou no walk");
    }

    public override void OnStateStay()
    {
        _player.Moving();

        if(_player.Moving() == Vector3.zero && _player.onGround)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.IDLE, _player);
        }

        if (_player.jumped)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.JUMPING, _player);
        }
    }

    public override void OnStateExit()
    {
        _player.animBase.PlayAnim(Animations.AnimationType.WALK, false);
        //Debug.Log("saiu do walk");
    }
}
public class Player_JumpingBehaviour : StateBase
{
    Player _player;
    public override void OnStateEnter(object o = null)
    {
        _player = (Player)o;
        _player.animBase.PlayAnim(Animations.AnimationType.JUMP, true);
        //Debug.Log("entrou no jump");
    }

    public override void OnStateStay()
    {
        _player.Moving();
        //Debug.Log("no ar");

        _player.jumped = false;

        PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.FALL, _player);
    }

    public override void OnStateExit()
    {
        //Debug.Log("saiu do jump");
    }
}

public class Player_Fall : StateBase
{
    Player _player;
    public override void OnStateEnter(object o = null)
    {
        _player = (Player)o;

        _player.animBase.PlayAnim(Animations.AnimationType.FALL, true);

        //Debug.Log("entrou na queda");
    }

    public override void OnStateStay()
    {
        if (_player.onGround)
        {
            //Debug.Log("no chao");
            
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.IDLE, _player);
        }
    }

    public override void OnStateExit()
    {
        //Debug.Log("no chão");
    }
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