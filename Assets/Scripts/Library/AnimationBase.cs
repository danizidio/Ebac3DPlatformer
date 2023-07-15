using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //public void PlayerAnimTrigger(AnimationType type)
        //{
        //    var setup = _animSetups.Find(x => x.animType == type);

        //    if (setup != null)
        //    {
        //        _anim.SetTrigger(setup.animType.ToString());
        //    }
        //}

        //public void PlayerAnimBool(AnimationType type,Nullable<bool> b)
        //{
        //    var setup = _animSetups.Find(x => x.animType == type);

        //    if (setup != null)
        //    {
        //        _anim.SetBool(setup.animType.ToString(),b);
        //    }
        //}

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
    }

    [Serializable]
    public class AnimationSetup
    {
        [SerializeField] AnimationType _animType;
        public AnimationType animType { get { return _animType; } }
    }
}
