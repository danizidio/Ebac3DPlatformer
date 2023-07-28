using Animations;
using CommonMethodsLibrary;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(AnimationBase))]
public class EnemyBase : MonoBehaviour, IDamageable
{
    public static Action OnSearchingPlayer;

    Rigidbody _rb;

    protected Player _player;

    AnimationBase _animBase;
    protected AnimationBase animBase { get { return _animBase; } }

    protected MeshRenderer _mr;

    [SerializeField] float _maxHealth;

    [SerializeField] int _attack;

    protected float _currentLife;

    [SerializeField] protected float _moveSpeed;

    bool _canMove;
    public bool canMove { get { return _canMove; } }

    protected Tween _currentTween;

    [SerializeField] Color32 _flashColor;

    [SerializeField] bool _useRandomColor;
    [SerializeField] protected Color _predominantColor;
    [SerializeField] protected Color _emissionColor;

    [SerializeField] ParticleSystem _hitFeedback;
    [SerializeField] VisualEffect[] _effectAsset;

    [SerializeField] float _distanceToAction;
    protected float _currentDistance;

    protected virtual void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _animBase = GetComponent<AnimationBase>();
        _mr = this.GetComponentInChildren<MeshRenderer>();
        _animBase.SetAnim(this.GetComponentInChildren<Animator>());

        if (_useRandomColor)
        {
            if (GameManager.OnChangeColor != null)
            {
                _predominantColor = GameManager.OnChangeColor.Invoke();
                _emissionColor = GameManager.OnChangeColor.Invoke();
            }
        }

        SetColors(_predominantColor, "_BaseColor");
        SetColors(_emissionColor, "_EmissionColor");

        foreach (VisualEffect effect in _effectAsset)
        {
            effect.Stop();
        }

        _currentLife = _maxHealth;
    }

    protected virtual void SetColors(Color color, string shaderProperty = null)
    {
        _hitFeedback.GetComponent<Renderer>().material.color = _predominantColor;
     
        if (shaderProperty != null)
        {
            _mr.material.SetColor(shaderProperty, color);
            _hitFeedback.GetComponent<Renderer>().material.SetColor(shaderProperty, color);
        }
        else
        {
            _mr.material.color = color;
        }
    }

    public virtual void DamageOutput(int damage, Vector3 pullFeedback)
    {
        _currentLife -= damage;

        _hitFeedback.Emit(10);

        transform.position -= pullFeedback;

        if (!_currentTween.IsActive())
        {
            _currentTween = DanUtils.MakeFlashColor(_mr.material, _flashColor, .1f,"_EmissionColor");
        }

        if(_currentLife <= 0)
        {
            GameManager.OnEnemyKilled();

            this.GetComponent<Rigidbody>().isKinematic = true;

            _animBase.PlayAnim(AnimationType.DEAD);

            foreach (VisualEffect effect in _effectAsset)
            {
                effect.Play();
            }

            _player = null;

            _hitFeedback.Emit(30);

            Destroy(this.gameObject, 4f);
        }
    }

    protected bool PlayerOnSight()
    {
        if (_player == null) return false;

        _currentDistance = Vector3.Distance(this.transform.position, _player.transform.position);

        if (_currentDistance <= _distanceToAction)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SearchPlayer()
    {
        StartCoroutine(SearchingPlayer());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        IDamageable p = collision.gameObject.GetComponent<IDamageable>();

        if(p != null)
        {
            p.DamageOutput(_attack);
        }
    }

    protected IEnumerator SearchingPlayer()
    {
        while (_player == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Player");

            if (temp != null)
            {
                _player = temp.GetComponent<Player>();
                StopCoroutine(SearchingPlayer());
            }

            yield return new WaitForSeconds(.5f);
        }
    }

    private void OnEnable()
    {
        OnSearchingPlayer += SearchPlayer;
    }
}
