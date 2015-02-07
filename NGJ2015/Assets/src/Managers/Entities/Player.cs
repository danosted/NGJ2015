using UnityEngine;
using System.Collections;
using Assets.src.Managers;

namespace Assets.src.Managers.Entities
{
    public class Player : CharacterBase
    {
        private bool isMoving;
        private Vector3 movement = Vector3.zero;

        public void OnEnable()
        {
            ManagerCollection.Instance.KeyInputManager.OnDownPressed += this.OnDownPressed;
            ManagerCollection.Instance.KeyInputManager.OnDownReleased += this.OnDownReleased;
            ManagerCollection.Instance.KeyInputManager.OnUpPressed += this.OnUpPressed;
            ManagerCollection.Instance.KeyInputManager.OnUpReleased += this.OnUpReleased;
            ManagerCollection.Instance.KeyInputManager.OnRightPressed += this.OnRightPressed;
            ManagerCollection.Instance.KeyInputManager.OnRightReleased += this.OnRightReleased;
            ManagerCollection.Instance.KeyInputManager.OnLeftPressed += this.OnLeftPressed;
            ManagerCollection.Instance.KeyInputManager.OnLeftReleased += this.OnLeftReleased;
            ManagerCollection.Instance.KeyInputManager.OnSpacePressed += this.OnSpacePressed;
            ManagerCollection.Instance.KeyInputManager.OnSpaceReleased += this.OnSpaceReleased;
        }

        public void OnDisable()
        {
            ManagerCollection.Instance.KeyInputManager.OnDownPressed -= this.OnDownPressed;
            ManagerCollection.Instance.KeyInputManager.OnDownReleased -= this.OnDownReleased;
            ManagerCollection.Instance.KeyInputManager.OnUpPressed -= this.OnUpPressed;
            ManagerCollection.Instance.KeyInputManager.OnUpReleased -= this.OnUpReleased;
            ManagerCollection.Instance.KeyInputManager.OnRightPressed -= this.OnRightPressed;
            ManagerCollection.Instance.KeyInputManager.OnRightReleased -= this.OnRightReleased;
            ManagerCollection.Instance.KeyInputManager.OnLeftPressed -= this.OnLeftPressed;
            ManagerCollection.Instance.KeyInputManager.OnLeftReleased -= this.OnLeftReleased;
            ManagerCollection.Instance.KeyInputManager.OnSpacePressed -= this.OnSpacePressed;
            ManagerCollection.Instance.KeyInputManager.OnSpaceReleased -= this.OnSpaceReleased;
        }

        private void OnDownPressed()
        {
            movement += Vector3.down;
            if(isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnDownReleased()
        {
            movement += Vector3.up;
        }
        private void OnUpPressed()
        {
            movement += Vector3.up;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnUpReleased()
        {
            movement += Vector3.down;
        }
        private void OnRightPressed()
        {
            movement += Vector3.right;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnRightReleased()
        {
            movement += Vector3.left;
        }
        private void OnLeftPressed()
        {
            movement += Vector3.left;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnLeftReleased()
        {
            movement += Vector3.right;
        }
        private void OnSpacePressed()
        {

        }
        private void OnSpaceReleased()
        {

        }

        private IEnumerator StartMoving()
        {
            isMoving = true;
            while (true)
            {
                if (movement.magnitude != 0f)
                {
                    StartMoving(movement*Time.deltaTime);
                }
                else
                {
                    isMoving = false;
                }
                yield return null;
            }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}