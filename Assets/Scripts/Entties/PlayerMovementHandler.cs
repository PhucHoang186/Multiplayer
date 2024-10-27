using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(RequireComponent))]
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] Rigidbody rig;
        [SerializeField] float moveSpeed;
        [SerializeField] float rotateSpeed;
        private Vector3 moveVec;
        private Vector3 rotateDir;
        private Camera mainCam;
        private Action<Vector3> onMovementUpdateCb;
        private bool moveable;

        void Start()
        {
            mainCam = Camera.main;
            SetMoveable(true);
        }

        public void InitCallbacks(Action<Vector3> onMovementUpdate)
        {
            onMovementUpdateCb = onMovementUpdate;
        }

        public void GetInput()
        {
            moveVec.x = Input.GetAxisRaw("Horizontal");
            moveVec.z = Input.GetAxisRaw("Vertical");
            moveVec.Normalize();

            rotateDir = GetRotateDir();
        }

        public void UpdateMovement()
        {
            if (!moveable)
                return;

            GetInput();
            ControlMovement();
        }

        void ControlMovement()
        {
            rig.velocity = moveVec * moveSpeed;

            if (moveVec != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVec), Time.deltaTime * rotateSpeed);
            }

            onMovementUpdateCb?.Invoke(moveVec);
        }

        public void SetMoveable(bool moveable)
        {
            this.moveable = moveable;
        }

        private Vector3 GetRotateDir()
        {
            Vector3 rotateDir = Vector3.zero;
            // if (Input.GetKey(KeyCode.J))
            //     rotateDir.x = -1;
            // else if (Input.GetKey(KeyCode.L))
            //     rotateDir.x = 1;

            // if (Input.GetKey(KeyCode.K))
            //     rotateDir.z = -1;
            // else if (Input.GetKey(KeyCode.I))
            //     rotateDir.z = 1;

            // Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y - transform.position.y);
            // Vector3 mousePosition = mainCam.ScreenToWorldPoint(mousePos);
            // rotateDir = (mousePosition - transform.position).normalized;

            return rotateDir;
        }

        public void OnGetHit(Vector3 dir, float force, float knockBackTime)
        {
            StartCoroutine(CorOnGetHit(dir, force, knockBackTime));
        }

        private IEnumerator CorOnGetHit(Vector3 dir, float force, float knockBackTime)
        {
            dir.y = 0f;
            transform.rotation = Quaternion.LookRotation(-dir);

            SetMoveable(false);
            rig.AddForce(dir * force, ForceMode.Impulse);

            yield return new WaitForSeconds(knockBackTime);
            SetMoveable(true);
            rig.velocity = Vector3.zero;
        }
    }
}