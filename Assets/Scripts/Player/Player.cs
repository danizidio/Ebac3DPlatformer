using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using StateMachines;
using Animations;
using NaughtyAttributes;

public class Player : MonoBehaviour, IDamageable
{
    public event Action OnPausing;

    [SerializeField] SO_Actors _playerAtributes;

    Rigidbody _rb;

    [SerializeField] AnimationBase _animBase;
    public AnimationBase animBase { get { return _animBase; } }

    bool _canMove;
    public bool canMove { get { return _canMove; } }

    [SerializeField] float _speedbonus;
    float _currentSpeed;

    [SerializeField] float _currentLife;

    PlayerLifebar _lifebar;

    float _xAxyz, _zAxyz;
    public float XAxyz { get { return _xAxyz; } set { _xAxyz = value; } }
    public float ZAxyz { get { return _zAxyz; } set { _zAxyz = value; } }

    bool _onGround;
    public bool onGround { get { return _onGround; } }

    bool _jumped;
    public bool jumped { get { return _jumped; } set { _jumped = value; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animBase = GetComponent<AnimationBase>();
        _animBase.SetAnim(this.GetComponentInChildren<Animator>());
        _lifebar = FindAnyObjectByType<PlayerLifebar>();
    }

    private void Start()
    {
        _canMove = true;

        _currentSpeed = _playerAtributes.walkSpeed;

        _currentLife = _playerAtributes.maxLife;

        PlayerStatesBehaviour.OnPlayerStateChange?.Invoke(PlayerStates.IDLE, this);
    }

    public Vector3 Moving()
    {
        Vector3 moveDirection = new Vector3(XAxyz, 0, ZAxyz).normalized;

        _rb.MovePosition(_rb.position + moveDirection * _currentSpeed * Time.deltaTime);
        
        Vector3 lookDirection = moveDirection + transform.position;

        transform.LookAt(lookDirection);

        return moveDirection;
    }

    #region - InputManager Buttons
    public void MovingLeftRight(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            _xAxyz = context.ReadValue<float>();
        }
    }

    public void MovingForwardBackward(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            _zAxyz = context.ReadValue<float>();
        }
    }

    public void Running(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            if(context.performed)
            {
                _currentSpeed = _playerAtributes.walkSpeed * _speedbonus;
                _animBase.GetAnim().speed = 1.5f;
            }
            if (context.canceled)
            {
                _currentSpeed = _playerAtributes.walkSpeed;
                _animBase.GetAnim().speed = 1;
            }
        }
    }
    public void Jumping(InputAction.CallbackContext context)
    {
        if(canMove && onGround)
        {
            Jumping();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPausing?.Invoke();
        }
    }

    #endregion

    public void Jumping()
    {
        _rb.AddForce(Vector3.up * _playerAtributes.jumpForce);
        _jumped = true;
    }

    public bool CanMove(bool b)
    {
        return _canMove = b;
    }

    public IEnumerator AnimGameOver()
    {
        _canMove = false;

        PlayerAnimation(AnimationType.DEAD);

        yield return new WaitForSeconds(2);
        
        //ADD CALL TO GAME OVER STATE
    }

    public void DamageOutput(int damage, Vector3 pullFeedback)
    {
        _currentLife -= damage;

        transform.position -= pullFeedback;

        _lifebar.onUpdateLifeBar?.Invoke(_currentLife, _playerAtributes.maxLife);

        if (_currentLife < 1)
        {
            AnimGameOver();
        }
    }

    public void PlayerAnimation(AnimationType animationType)
    {
        _animBase.PlayAnim(animationType);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("GROUND"))
        {
            _onGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("GROUND"))
        {
            _onGround = false;
        }
    }
}
