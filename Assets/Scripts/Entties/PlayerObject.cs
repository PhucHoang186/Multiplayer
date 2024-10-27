using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Entity
{
    public enum PlayerState
    {
        Normal,
        Attack,
        Hit,
        Invincible,

    }

    public class PlayerObject : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] PlayerAnimationHandler animationHandler;
        [SerializeField] PlayerMovementHandler movementHandler;
        [SerializeField] PlayerEffectHandler effectHandler;
        [SerializeField] CollissionDetection collissionDetection;
        [Header("Values")]
        [SerializeField] float knockBackForce;
        [SerializeField] float knockBackTime;

        private PlayerState currentState;

        public override void OnNetworkSpawn()
        {
            SetState(PlayerState.Normal);
            base.OnNetworkSpawn();
            if (!IsLocalPlayer)
            {
                Destroy(movementHandler);
                Destroy(animationHandler);
            }
            else
            {
                movementHandler.InitCallbacks(OnMovementUpdate);
            }
        }

        public void OnGetHit(Vector3 dir)
        {
            StartCoroutine(CorOnGetHit(dir));
        }

        private IEnumerator CorOnGetHit(Vector3 dir)
        {
            SetState(PlayerState.Hit);
            movementHandler.OnGetHit(dir, knockBackForce, knockBackTime);
            animationHandler.OnGetHit();
            effectHandler.OnGetHit();

            yield return new WaitForSeconds(0.7f);
            SetState(PlayerState.Normal);
        }

        public void Update()
        {
            if (!IsLocalPlayer)
                return;

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (!IsState(PlayerState.Normal))
                    return;

                SetState(PlayerState.Attack);
                animationHandler?.PlayAnim(PlayerAnim.Attack, finishCb: () => SetState(PlayerState.Normal));
            }
        }

        private void FixedUpdate()
        {
            if (!IsLocalPlayer)
                return;

            movementHandler.UpdateMovement();
        }

        public void OnMovementUpdate(Vector3 movement)
        {
            if (IsState(PlayerState.Hit) || IsState(PlayerState.Attack))
                return;
            if (movement != Vector3.zero)
                animationHandler?.PlayAnim(PlayerAnim.Move);
            else
                animationHandler?.PlayAnim(PlayerAnim.Idle);
        }

        private bool IsState(PlayerState state)
        {
            return currentState == state;
        }

        private void SetState(PlayerState state)
        {
            this.currentState = state;
        }
    }
}
