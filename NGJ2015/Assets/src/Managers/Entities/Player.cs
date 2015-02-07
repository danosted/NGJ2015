using UnityEngine;
using System.Collections;
using Assets.src.Managers;
using Assets.src.Common;

namespace Assets.src.Managers.Entities
{
    public class Player : CharacterBase
    {
        private bool isMoving;
        private Vector3 movement = Vector3.zero;
		private Weapon weapon = new Weapon();

        public void OnEnable()
        {
            KeyInputHandler.Instance.OnDownPressed += this.OnDownPressed;
            KeyInputHandler.Instance.OnDownReleased += this.OnDownReleased;
            KeyInputHandler.Instance.OnUpPressed += this.OnUpPressed;
            KeyInputHandler.Instance.OnUpReleased += this.OnUpReleased;
            KeyInputHandler.Instance.OnRightPressed += this.OnRightPressed;
            KeyInputHandler.Instance.OnRightReleased += this.OnRightReleased;
            KeyInputHandler.Instance.OnLeftPressed += this.OnLeftPressed;
            KeyInputHandler.Instance.OnLeftReleased += this.OnLeftReleased;
            KeyInputHandler.Instance.OnSpacePressed += this.OnSpacePressed;
            KeyInputHandler.Instance.OnSpaceReleased += this.OnSpaceReleased;
        }

        public void OnDisable()
        {
            if(!KeyInputHandler.Instance) return;
            KeyInputHandler.Instance.OnDownPressed -= this.OnDownPressed;
            KeyInputHandler.Instance.OnDownReleased -= this.OnDownReleased;
            KeyInputHandler.Instance.OnUpPressed -= this.OnUpPressed;
            KeyInputHandler.Instance.OnUpReleased -= this.OnUpReleased;
            KeyInputHandler.Instance.OnRightPressed -= this.OnRightPressed;
            KeyInputHandler.Instance.OnRightReleased -= this.OnRightReleased;
            KeyInputHandler.Instance.OnLeftPressed -= this.OnLeftPressed;
            KeyInputHandler.Instance.OnLeftReleased -= this.OnLeftReleased;
            KeyInputHandler.Instance.OnSpacePressed -= this.OnSpacePressed;
            KeyInputHandler.Instance.OnSpaceReleased -= this.OnSpaceReleased;
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
			weapon.Attack (transform, Enumerations.WeaponType.Club);
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
                //else
                //{
                //    isMoving = false;
                //}
                yield return null;
            }
        }

        public override void Die()
        {
            //Debug.Log("Player die");
            base.Die();
            
        }
    }
}