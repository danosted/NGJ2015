using UnityEngine;
using System.Collections;
using Assets.src.Managers;
using Assets.src.Common;
using Assets.src.Input;

namespace Assets.src.Managers.Entities
{
    public class Player : CharacterBase
    {
        private bool isMoving;
        private Vector3 movement = Vector3.zero;
		public string playerName;
		private Weapon weapon = new Weapon();

        private long _points = 0;

        public long GetPoints()
        {
            return _points;
        }

        public void AddPoints(long points)
        {
            _points += points;
        }

        public void UseGamePad1()
		{
			Debug.Log ("Player 1 Events added");
			KeyInputHandler.Instance.OnJoy1Vertical += this.OnJoy1Vertical;
			KeyInputHandler.Instance.OnJoy1Horizontal += this.OnJoy1Horizontal;
			KeyInputHandler.Instance.OnJoy1FirePressed += this.OnJoy1FirePressed;
			
			KeyInputHandler.Instance.OnMovementStop += this.OnMovementStop;	
			this.GetComponent<MouseLook2D> ().UseGamepad = true;
			this.GetComponent<MouseLook2D> ().PlayerNr = 1;
		}
		public void UseGamePad2()
		{
			Debug.Log ("Player 2 Events added");
			KeyInputHandler.Instance.OnJoy2Vertical += this.OnJoy2Vertical;
			KeyInputHandler.Instance.OnJoy2Horizontal += this.OnJoy2Horizontal;
			KeyInputHandler.Instance.OnJoy2FirePressed += this.OnJoy2FirePressed;
			
			KeyInputHandler.Instance.OnMovementStop += this.OnMovementStop;	
			this.GetComponent<MouseLook2D> ().UseGamepad = true;
			this.GetComponent<MouseLook2D> ().PlayerNr = 2;
		}
		public void UseKeyBoard()
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
		
		public void OnEnable()
		{
//			KeyInputHandler.Instance.OnDownPressed += this.OnDownPressed;
//            KeyInputHandler.Instance.OnDownReleased += this.OnDownReleased;
//            KeyInputHandler.Instance.OnUpPressed += this.OnUpPressed;
//            KeyInputHandler.Instance.OnUpReleased += this.OnUpReleased;
//            KeyInputHandler.Instance.OnRightPressed += this.OnRightPressed;
//            KeyInputHandler.Instance.OnRightReleased += this.OnRightReleased;
//            KeyInputHandler.Instance.OnLeftPressed += this.OnLeftPressed;
//            KeyInputHandler.Instance.OnLeftReleased += this.OnLeftReleased;
//            KeyInputHandler.Instance.OnSpacePressed += this.OnSpacePressed;
//            KeyInputHandler.Instance.OnSpaceReleased += this.OnSpaceReleased;
//			KeyInputHandler.Instance.OnMovementStop += this.OnMovementStop;
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

		private void OnJoy1Horizontal(float mag)
		{
			//Debug.Log (playerName);
			movement += Vector3.right*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
		}
		private void OnJoy1Vertical(float mag)
		{
			//Debug.Log (playerName);
			movement += Vector3.up*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
		}
		private void OnJoy1FirePressed()
		{			
			//Debug.Log ("Attack1");
			weapon.Attack (transform, Enumerations.WeaponType.Club);
		}
		
		private void OnJoy2Horizontal(float mag)
		{
			//Debug.Log (playerName);
			movement += Vector3.right*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
		}
		private void OnJoy2Vertical(float mag)
		{
			//Debug.Log (playerName);
			movement += Vector3.up*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
		}
		private void OnJoy2FirePressed()
		{			
			//Debug.Log ("Attack2");
			weapon.Attack (transform, Enumerations.WeaponType.Club);
		}

		private void OnDownPressed(float mag)
        {
			movement += Vector3.down*mag;
            if(isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnDownReleased(float mag)
        {
			movement += Vector3.up*mag;
        }
		private void OnUpPressed(float mag)
        {
			movement += Vector3.up*mag;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnUpReleased(float mag)
        {
			movement += Vector3.down*mag;
        }
        private void OnRightPressed(float mag)
        {
			movement += Vector3.right*mag;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnRightReleased(float mag)
        {
			movement += Vector3.left*mag;
        }
        private void OnLeftPressed(float mag)
        {
            movement += Vector3.left*mag;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnLeftReleased(float mag)
        {
            movement += Vector3.right*mag;
		}
		private void OnMovementStop()
		{
			movement = Vector3.zero;
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
				float newx = transform.position.x + movement.x;
				float newy = transform.position.y + movement.y;
                if (movement.magnitude != 0f)
                {
					var newMovement = movement;
					if (newx > 30 || newx < -30) {
						newMovement.x = 0;
					}
					if (newy > 13 || newy < -11) {
						newMovement.y = 0;
					}
                    StartMoving(newMovement*Time.deltaTime);
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