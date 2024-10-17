using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(RequireComponent))]
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] float moveSpeed;
        [SerializeField] float rotateSpeed;
        private Vector3 moveVec;
        private Vector3 rotateDir;
        private Camera mainCam;
        private Action<Vector3> onMovementUpdateCb;

        void Start()
        {
            mainCam = Camera.main;
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

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y - transform.position.y);
            Vector3 mousePosition = mainCam.ScreenToWorldPoint(mousePos);
            rotateDir = (mousePosition - transform.position).normalized;
        }

        public void UpdateMovement()
        {
            GetInput();
            ControlMovement();
        }

        void ControlMovement()
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + moveVec * moveSpeed, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotateDir), Time.deltaTime * rotateSpeed);
            onMovementUpdateCb?.Invoke(moveVec);
        }
    }
}