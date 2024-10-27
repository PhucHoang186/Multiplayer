using System.Collections;
using System.Collections.Generic;
using Entity;
using Unity.Netcode;
using UnityEngine;

public class Ball : NetworkBehaviour
{
   [SerializeField] Rigidbody rig;
   private bool isToggle;

   private void OnCollisionEnter(Collision collision)
   {
      if (isToggle)
      {
         if (collision.gameObject.TryGetComponent<PlayerObject>(out var player))
         {
            player.OnGetHit((player.transform.position - transform.position).normalized);
            isToggle = false;
         }
      }
   }

   public void AddForce(float force, Vector3 direction)
   {
      isToggle = true;
      direction.y = 0f;
      rig.AddForce(direction * force, ForceMode.Impulse);
   }
}
