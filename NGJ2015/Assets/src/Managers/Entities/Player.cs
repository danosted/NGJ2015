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
		private bool isDead;
        private Vector3 movement = Vector3.zero;
		public string playerName;
        private Weapon weapon;

        public int gamePad;
        
        [SerializeField]
        private bool isUsingGamePad;
        private float deadTime = 5f;
        private float deadTimer = 0;

        private Vector3 _initialPosition;

        public bool IsDead()
        {
            return isDead;
        }

        private long _points = 0;

        public long GetPoints()
        {
            return _points;
        }

        public void AddPoints(long points)
        {
            _points += points;
        }

        public void setInitialPosition(Vector3 pos)
        {
            _initialPosition = pos;
            transform.position = _initialPosition;
        }

        public new void FixedUpdate()
        {
            base.FixedUpdate();
            if (isDead)
            {
                deadTimer += Time.fixedDeltaTime;

                if (deadTimer > deadTime)
                {
                    Debug.LogError(Time.time+" Reviving "+gameObject);
                    isDead = false;
                    deadTimer = 0;
                    var anim = GetComponentInChildren<Animator>();
                    anim.SetBool("isDead", false);
                    _health = _initialhealth;
                    healthbar.Init(_health);
                    transform.position = _initialPosition;
                    StartCoroutine(StartMoving());
                }
            }
        }


        public void UseGamePad1()
        {
            gamePad = 1;
            GetComponent<MouseLook2D>().PlayerNr = gamePad;
            GetComponent<JoypadInputhandler>().gamePad = gamePad;
            isUsingGamePad = true;
			Debug.Log ("Player 1 Events added");
            //KeyInputHandler.Instance.OnJoy1Vertical += this.OnJoy1Vertical;
            //KeyInputHandler.Instance.OnJoy1Horizontal += this.OnJoy1Horizontal;
            //KeyInputHandler.Instance.OnJoy1FirePressed += this.OnJoy1FirePressed;
			
            //KeyInputHandler.Instance.OnMovementStop += this.OnMovementStop;	
            //this.GetComponent<MouseLook2D> ().UseGamepad = true;
            //this.GetComponent<MouseLook2D> ().PlayerNr = 1;
		}
		public void UseGamePad2()
		{
		    gamePad = 2;
		    GetComponent<MouseLook2D>().PlayerNr = gamePad;
            GetComponent<JoypadInputhandler>().gamePad = gamePad;
		    isUsingGamePad = true;
			Debug.Log ("Player 2 Events added");
            //KeyInputHandler.Instance.OnJoy2Vertical += this.OnJoy2Vertical;
            //KeyInputHandler.Instance.OnJoy2Horizontal += this.OnJoy2Horizontal;
            //KeyInputHandler.Instance.OnJoy2FirePressed += this.OnJoy2FirePressed;
			
            //KeyInputHandler.Instance.OnMovementStop += this.OnMovementStop;	
            //this.GetComponent<MouseLook2D> ().UseGamepad = true;
            //this.GetComponent<MouseLook2D> ().PlayerNr = 2;
		}
		public void UseKeyBoard()
		{
            gamePad = 0;
            GetComponent<MouseLook2D>().PlayerNr = gamePad;
		    GetComponent<JoypadInputhandler>().enabled = false;
		    isUsingGamePad = false;
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

        //public void OnJoy1Horizontal(float mag)
        //{
        //    if (!isUsingGamePad) return;
        //    Debug.Log (playerName);
        //    movement += Vector3.right*mag;
        //    if(isMoving) return;
        //    StartCoroutine(StartMoving());			
        //}
        //public void OnJoy1Vertical(float mag)
        //{
        //    if (!isUsingGamePad) return;
        //    //Debug.Log (playerName);
        //    movement += Vector3.up*mag;
        //    if(isMoving) return;
        //    StartCoroutine(StartMoving());			
        //}

        public void GamePadDirection(Vector3 direction)
        {
            movement = direction;
            if(isMoving) return;
            StartCoroutine(StartMoving());	
        }

        public void OnJoy1FirePressed()
		{
            if (!isUsingGamePad) return;
			//Debug.Log ("Attack1");
		    if (!weapon)
		    {
		        weapon = GetComponent<Weapon>();
		    }
			weapon.Attack (transform, Enumerations.WeaponType.Club);
			var anim = GetComponentInChildren<Animator> ();
			anim.SetTrigger ("attack");

		}

        public void OnJoy2Horizontal(float mag)
		{
            if (!isUsingGamePad) return;
			Debug.Log (playerName);
			movement += Vector3.right*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
		}
        public void OnJoy2Vertical(float mag)
		{
            if (!isUsingGamePad) return;
			//Debug.Log (playerName);
			movement += Vector3.up*mag;
			if(isMoving) return;
			StartCoroutine(StartMoving());			
		}
        public void OnJoy2FirePressed()
		{
            if (!isUsingGamePad) return;
			weapon.Attack (transform, Enumerations.WeaponType.Club);
		}

		private void OnDownPressed(float mag)
        {
            if (isUsingGamePad) return;
			movement += Vector3.down*mag;
            if(isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnDownReleased(float mag)
        {
            if (isUsingGamePad) return;
			movement += Vector3.up*mag;
        }
		private void OnUpPressed(float mag)
        {
            if (isUsingGamePad) return;
			movement += Vector3.up*mag;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnUpReleased(float mag)
        {
            if (isUsingGamePad) return;
			movement += Vector3.down*mag;
        }
        private void OnRightPressed(float mag)
        {
            if (isUsingGamePad) return;
			movement += Vector3.right*mag;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnRightReleased(float mag)
        {
            if (isUsingGamePad) return;
			movement += Vector3.left*mag;
        }
        private void OnLeftPressed(float mag)
        {
            if (isUsingGamePad) return;
            movement += Vector3.left*mag;
            if (isMoving) return;
            StartCoroutine(StartMoving());
        }
		private void OnLeftReleased(float mag)
        {
            if (isUsingGamePad) return;
            movement += Vector3.right*mag;
		}
		private void OnMovementStop()
		{
			movement = Vector3.zero;
		}
        private void OnSpacePressed()
        {
            if (isUsingGamePad) return;
            if (!weapon)
            {
                weapon = GetComponent<Weapon>();
            }
            weapon.Attack(transform, Enumerations.WeaponType.Club);
			if (!isDead) {
				Animator anim = GetComponentInChildren<Animator> ();
				anim.SetTrigger ("attack");
				if (!weapon) {
					weapon = GetComponent<Weapon> ();
				}
				weapon.Attack (transform, Enumerations.WeaponType.Club);
			}
        }

		private void OnSpaceReleased()
        {

        }

		


        private IEnumerator StartMoving()
		{
			var anim = GetComponentInChildren<Animator>();
            isMoving = true;
            while (!isDead)
            {
				float newx = transform.position.x + movement.x;
				float newy = transform.position.y + movement.y;
                if (movement.magnitude != 0f) {
					anim.SetBool("walking", true);
					var newMovement = movement;
					if (newx > 30 || newx < -30) {
						newMovement.x = 0;
					}
					if (newy > 13 || newy < -11) {
						newMovement.y = 0;
					}
                    StartMoving(newMovement*Time.deltaTime);
                } else {
					anim.SetBool("walking", false);
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
            Debug.LogError(Time.time + "Player die "+gameObject);
			var anim = GetComponentInChildren<Animator> ();
			isDead = true;
			anim.SetBool ("isDead", isDead);
			base.Die();
			//ManagerCollection.Instance.PlayerManager.LoseGameBoth();
        }
    }
}