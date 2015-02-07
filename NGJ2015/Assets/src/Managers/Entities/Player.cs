using UnityEngine;
using System.Collections;
using Assets.src.Managers;

namespace Assets.src.Managers.Entities
{
    public class Player : CharacterBase
    {
        private bool isMoving;
        private Vector3 movement = Vector3.zero;

        public Player(float health, float speed, float range, float damage)
            : base(health, speed, range, damage)
        {

        }

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
            Debug.Log("Hek");
            movement += Vector3.down;
            Debug.Log("p " + movement);
            if(isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnDownReleased()
        {
            movement += Vector3.up;
            Debug.Log("r " + movement);
        }
        private void OnUpPressed()
        {
            movement += Vector3.up;
            Debug.Log("p " + movement);
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnUpReleased()
        {
            movement += Vector3.down;
            Debug.Log("r " + movement);
        }
        private void OnRightPressed()
        {
            movement += Vector3.right;
            Debug.Log("p " + movement);
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnRightReleased()
        {
            movement += Vector3.left;
            Debug.Log("r " + movement);
        }
        private void OnLeftPressed()
        {
            movement += Vector3.left;
            Debug.Log("p " + movement);
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
        private void OnLeftReleased()
        {
            movement += Vector3.right;
            Debug.Log("r " + movement);
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
            Debug.Log("#Sup " + movement.magnitude);
            while (true)
            {
                if (movement.magnitude != 0f)
                {
                    StartMoving(movement*Time.deltaTime);
                }
                yield return null;
            }
            isMoving = false;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}