using Animations;
using CommonMethodsLibrary;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(AnimationBase))]
public class EnemyBase : MonoBehaviour, IDamageable
{
    Rigidbody _rb;

    protected Player _player;

    AnimationBase _animBase;
    protected AnimationBase animBase { get { return _animBase; } }

    [SerializeField] float _maxHealth;

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

    protected MeshRenderer _mr;

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
        if (shaderProperty != null)
        {
            _mr.material.SetColor(shaderProperty, color);
            _hitFeedback.GetComponent<Renderer>().material.SetColor(shaderProperty, color);
        }
        else
        {
            _hitFeedback.GetComponent<Renderer>().material.color = _predominantColor;
            _mr.material.color = color;
        }
    }

    public void DamageOutput(int damage, Vector3 pullFeedback)
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

    protected void OnCollisionEnter(Collision collision)
    {
        

    }

}
