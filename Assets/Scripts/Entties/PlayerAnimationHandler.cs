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

        public void PlayAnim(PlayerAnim animType)
        {
            anim?.Play(animType.ToString());
        }
    }
}
