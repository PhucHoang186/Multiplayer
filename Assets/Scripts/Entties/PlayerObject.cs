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

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if(!IsLocalPlayer)
            {
                Destroy(movementHandler);
                Destroy(animationHandler);
            }
        }

        public void Update()
        {
            if(!IsLocalPlayer)
            return;


            if(Input.GetMouseButtonDown(0))
            {
                animationHandler?.PlayAnim(PlayerAnim.Attack);
            }
        }
    }
}
