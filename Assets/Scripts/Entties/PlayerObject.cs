using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Entity
{
    public class PlayerObject : NetworkBehaviour
    {
        [SerializeField] PlayerAnimationHandler animationHandler;
        [SerializeField] PlayerMovementHandler movementHandler;
        private bool moveableState;

        public override void OnNetworkSpawn()
        {
            moveableState = true;
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

        public void Update()
        {
            if (!IsLocalPlayer)
                return;

            movementHandler.UpdateMovement();

            if (Input.GetMouseButtonDown(0))
            {
                if (!moveableState)
                    return;
                moveableState = false;
                animationHandler?.PlayAnim(PlayerAnim.Attack, finishCb: () => moveableState = true);
            }
        }

        public void OnMovementUpdate(Vector3 movement)
        {
            if (!moveableState)
                return;
            if (movement != Vector3.zero)
                animationHandler?.PlayAnim(PlayerAnim.Move);
            else
                animationHandler?.PlayAnim(PlayerAnim.Idle);
        }
    }
}
