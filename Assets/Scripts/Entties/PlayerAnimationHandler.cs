using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public enum PlayerAnim
    {
        Idle,
        Move,
        Attack,
        Hit,
    }

    public class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] Animator anim;
        private PlayerAnim currentAnimType;

        public void PlayAnim(PlayerAnim animType, float transitionTime = 0.1f, Action finishCb = null)
        {
            if (currentAnimType == animType)
                return;
            currentAnimType = animType;
            anim?.CrossFade(animType.ToString(), transitionTime);
            WaitForAnimToFinish(finishCb);
        }

        private void WaitForAnimToFinish(Action finishCb)
        {
            StartCoroutine(CorWaitForAnimToFinish(finishCb));
        }

        private IEnumerator CorWaitForAnimToFinish(Action finishCb)
        {
            var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSecondsRealtime(stateInfo.length);
            finishCb?.Invoke();
        }

        public void OnGetHit()
        {
            PlayAnim(PlayerAnim.Hit);
        }
    }
}
