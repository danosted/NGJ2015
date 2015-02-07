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

        public void UseGamePad()
		{
			KeyInputHandler.Instance.OnVertical += this.OnVertical;
			KeyInputHandler.Instance.OnHorizontal += this.OnHorizontal;
			KeyInputHandler.Instance.OnFirePressed += this.OnFirePressed;
			KeyInputHandler.Instance.OnMovementStop += this.OnMovementStop;			
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

		private void OnHorizontal(float mag)
		{
			movement += Vector3.right*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
		}
		private void OnVertical(float mag)
		{
			movement += Vector3.up*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
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

		
		private void OnFirePressed()
		{
			iTween.PunchRotation(transform.GetChild (0).GetChild(3).gameObject, new Vector3(0, 0, -120),0.5f);
			iTween.ShakePosition(Camera.main.gameObject, Vector3.one*0.02f, 0.5f);
			//			iTween.PunchRotation(Camera.main.gameObject, new Vector3(0, 0, 720),4.5f);
			var colliders = Physics.OverlapSphere(transform.position, 10f);
			foreach(var collider in colliders)
			{
				iTween.PunchScale(collider.gameObject, Vector3.one*10.1f, 0.5f);
			}
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