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
        _player.animBase.PlayAnim(Animations.AnimationType.FALL, false);
    }

    public override void OnStateStay()
    {
        if (_player.ZAxyz != 0 && _player.onGround)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.WALKING, _player);
        }

        if (_player.jumped)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.JUMPING, _player);
        }

        if (!_player.onGround)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.FALL, _player);
        }
    }

    public override void OnStateExit()
    {

    }

}

public class Player_WalkingBehaviour : StateBase
{
    Player _player;

    public override void OnStateEnter(object o = null)
    {
        _player = o as Player;
        _player.animBase.PlayAnim(Animations.AnimationType.WALK, true);

        CameraBehaviour.OnShakeCam?.Invoke(.1f, .1f, 0);
    }

    public override void OnStateStay()
    {    
        if(_player.ZAxyz == 0 && _player.onGround)
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
        CameraBehaviour.OnShakeCam?.Invoke(0, 0, 0);

        _player.animBase.PlayAnim(Animations.AnimationType.WALK, false);
    }
}
public class Player_JumpingBehaviour : StateBase
{
    Player _player;
    public override void OnStateEnter(object o = null)
    {
        _player = o as Player;
        _player.animBase.PlayAnim(Animations.AnimationType.JUMP);
        _player.animBase.PlayAnim(Animations.AnimationType.FALL, true);
    }

    public override void OnStateStay()
    {
        if (_player.onGround)
        {
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.IDLE, _player);
        }
    }

    public override void OnStateExit()
    {
        _player.jumped = false;
        _player.animBase.PlayAnim(Animations.AnimationType.FALL, false);
    }
}

public class Player_Fall : StateBase
{
    Player _player;
    public override void OnStateEnter(object o = null)
    {
        _player = o as Player;

        _player.animBase.PlayAnim(Animations.AnimationType.FALL, true);
    }

    public override void OnStateStay()
    {
        if (_player.onGround)
        {            
            PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.IDLE, _player);
        }
    }

    public override void OnStateExit()
    {
        _player.animBase.PlayAnim(Animations.AnimationType.FALL, false);
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
    public override void OnStateEnter(object o = null)
    {
        MessageLog.OnCallMessage("Dead... Respawning!");
        GameplaySateMachine.OnGameStateChange(GameStates.GAMEOVER, o);
    }
}