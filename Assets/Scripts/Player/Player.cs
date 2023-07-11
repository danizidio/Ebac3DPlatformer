using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using StateMachines;

public class Player : MonoBehaviour, IDamageable
{
    public event Action OnActing;
    public event Action OnPausing;

    [SerializeField] SO_Actors _playerAtributes;
    [SerializeField] Transform _footDetector;

    Rigidbody _rb;
    [SerializeField] Animator _anim;
    public Animator anim { get { return _anim; }set { _anim = value; } }

    PlayerStatesBehaviour _playerStates;

    [SerializeField] bool _canMove;
    public bool canMove { get { return _canMove; } }

    [SerializeField] float _speedbonus = 1;

    [SerializeField] float _currentLife;

    float _xAxyz, _zAxyz;
    public float XAxyz { get { return _xAxyz; } set { _xAxyz = value; } }
    public float ZAxyz { get { return _zAxyz; } set { _zAxyz = value; } }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerStates = GetComponent<PlayerStatesBehaviour>();
    }

    private void Start()
    {
        _currentLife = _playerAtributes.maxLife;
    }

    void LateUpdate()
    {
        if (_canMove)
        {
            if((XAxyz == 0 &&  ZAxyz == 0) && IsOnGround())
            {
                _playerStates.statemachine.SwitchState(PlayerStates.IDLE);
            }

            Moving();
        }
    }

    void Moving()
    {      
        Vector3 moveDirection =  new Vector3(XAxyz, 0, ZAxyz).normalized;

       _rb.MovePosition(_rb.position +moveDirection * _playerAtributes.walkSpeed * Time.deltaTime);

        Vector3 lookDirection = moveDirection +  transform.position;

        transform.LookAt(lookDirection);
    }

    #region - InputManager Buttons
    public void MovingLeftRight(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            _xAxyz = context.ReadValue<float>();

            _playerStates.statemachine.SwitchState(PlayerStates.WALKING);
        }
    }

    public void MovingForwardBackward(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            _zAxyz = context.ReadValue<float>();

            _playerStates.statemachine.SwitchState(PlayerStates.WALKING);
        }
    }
    public void Jumping(InputAction.CallbackContext context)
    {
        if(canMove && IsOnGround())
        {
            _rb.AddForce(Vector3.up * _playerAtributes.jumpForce);
            _playerStates.statemachine.SwitchState(PlayerStates.JUMPING);
        }
    }
    public void OnInteracting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnActing?.Invoke();
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

    public bool CanMove(bool b)
    {
        return _canMove = b;
    }

    public IEnumerator AnimGameOver()
    {
        _canMove = false;

        _anim.SetTrigger("DEAD");

        yield return new WaitForSeconds(2);
        
        //ADD CALL TO GAME OVER STATE
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            _anim.SetBool("JUMP", false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            _anim.SetBool("JUMP", true);
        }
    }

    public void DamageOutput(int damage)
    {

    }

    public bool IsOnGround()
    {
        return (Physics.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("GROUND")) |
               (Physics.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("FLOATINGPLATFORM"))));
    }
}
