using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Animations
{
    public enum AnimationType
    {
          NONE
        , IDLE
        , WALK
        , ATTACK
        , JUMP
        , DEAD

        //ESPECIFICS
         , FALL
    }

    public class AnimationBase : MonoBehaviour
    {
        [SerializeField] Animator _anim;

        [SerializeField] List<AnimationSetup> _animSetups;

        float _defaultAnimSpeed = 1;

        public void PlayAnim(AnimationType type, Nullable<bool> b = null)
        {
            var setup = _animSetups.Find(x => x.animType == type);

            if (setup == null) return;

            if (b != null)
            {
                _anim.SetBool(setup.animType.ToString(), (bool)b);
            }
            else
            {
                _anim.SetTrigger(setup.animType.ToString());
            }
        }

        public void SetAnim(Animator anim)
        {
            _anim = anim;
        }

        public Animator GetAnim()
        {
            return _anim;
        }

        public void SetAnimSpeed(float spd)
        {
            _anim.speed = spd;
        }

        public void SetDefaultAnimSpeed()
        {
            _anim.speed = _defaultAnimSpeed;
        }

        public void SetFloatAnim(string stringParam, float value)
        {
            _anim.SetFloat(stringParam, value);
        }
    }

    [Serializable]
    public class AnimationSetup
    {
        [SerializeField] AnimationType _animType;
        public AnimationType animType { get { return _animType; } }
    }
}
