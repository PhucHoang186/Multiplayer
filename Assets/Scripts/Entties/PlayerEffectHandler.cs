using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class PlayerEffectHandler : MonoBehaviour
    {
        [SerializeField] ParticleSystem hitVfx;

        public void OnGetHit()
        {
            hitVfx.Stop();
            hitVfx.Play();
        }
    }
}