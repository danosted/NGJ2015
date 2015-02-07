using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Input
{
    public class MouseLook2D : MonoBehaviour
    {
        private Transform model;
        private Transform body;
        private bool facingLeft;
        private bool _isFacingRight;
		public bool UseGamepad = false;
		public int PlayerNr = 1;
		
		public delegate void OnLookVerticalDelegate(float mag);
		public event OnLookVerticalDelegate OnLookVertical;
		
		public delegate void OnLookHorizontalDelegate(float mag);
		public event OnLookVerticalDelegate OnLookHorizontal;

        void Awake()
        {
            MouseInputHandler input = GetComponent<MouseInputHandler>();
            //input.OnPress += OnPressed;
            //input.OnRelease += OnReleased;
#if UNITY_ANDROID
		input.OnFaceLeft += FaceLeft;
		input.OnFaceRight += FaceRight;
#endif
            body = transform.FindChild(Constants.TransformBodyName);
            Debug.LogWarning(transform.GetChild(0).childCount);
            Debug.LogWarning(body);
            //body = weapons[0];
            //GameObject weaponGO = Instantiate(body.gameObject, gunPosition.position, body.transform.rotation) as GameObject;
            //weaponGO.transform.parent = transform;
            //body = weaponGO.GetComponent<Weapon>();
            //gameManInstance = GameManager.Instance;
            //gameManInstance.OnStateChanged += OnStateChange;
            //GetComponent<GameInitializer2Object>().OnInitializeWithDependencies += Initialize;
            //		GameObject ch = Instantiate(crossHairs.gameObject) as GameObject;
            //		this.crossHairs = ch.transform;
            //		Screen.showCursor = false;
            StartCoroutine(PointGun());
        }

        public void Initialize(GameObject[] dependencies)
        {
            //healthBar = dependencies[0].GetComponent<HealthbarScript>();
            //healthBar.Init(health);
        }
#if UNITY_ANDROID

#else
        void Update()
        {
			if (!UseGamepad) {
				if (Camera.main.ScreenToWorldPoint (UnityEngine.Input.mousePosition).x < transform.position.x) {
						FaceLeft ();
				} else {
						FaceRight ();
				}
			} else {
				if(PlayerNr == 1){
					if(UnityEngine.Input.GetAxis("Joy1-Look-Horizontal") < 0)
					{
							FaceLeft ();
						
					}
					if(UnityEngine.Input.GetAxis("Joy1-Look-Horizontal") > 0)
					{
							FaceRight ();
						
					}
				} else {
					if(UnityEngine.Input.GetAxis("Joy2-Look-Horizontal") < 0)
					{
							FaceLeft ();
						
					}
					if(UnityEngine.Input.GetAxis("Joy2-Look-Horizontal") > 0)
					{
							FaceRight ();
						
					}
				}
			}
        }
#endif

        private void FaceLeft()
        {
            if (!facingLeft)
            {
                facingLeft = true;
                float scale_x = transform.localScale.x;
                transform.localScale = new Vector3(-1f * scale_x, transform.localScale.y, transform.localScale.z);
            }
        }

        private void FaceRight()
        {
            if (facingLeft)
            {
                facingLeft = false;
                float scale_x = transform.localScale.x;
                transform.localScale = new Vector3(-1f * scale_x, transform.localScale.y, transform.localScale.z);
            }
        }

        private IEnumerator PointGun()
        {
            while (body)
            {
				Vector3 mousepos = Vector3.zero;
				if(!UseGamepad) {
                	mousepos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				} else {
					Debug.Log (PlayerNr);
					if(PlayerNr == 1){						
						mousepos += Vector3.up * UnityEngine.Input.GetAxis("Joy1-Look-Vertical") * 360;
						mousepos += Vector3.right * UnityEngine.Input.GetAxis("Joy1-Look-Horizontal") * 360;
					} else {						
						mousepos += Vector3.up * UnityEngine.Input.GetAxis("Joy2-Look-Vertical") * 360;
						mousepos += Vector3.right * UnityEngine.Input.GetAxis("Joy2-Look-Horizontal") * 360;
					}
				}
                //			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
                Vector3 weaponPos = body.transform.position;
                Vector3 weaponToMouse = (mousepos - weaponPos).normalized;
                float angle = Mathf.Atan(weaponToMouse.y / weaponToMouse.x);
                if (facingLeft)
                {
                    body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.back);
                }
                else
                {
                    body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.forward);
                }
                yield return null;
            }
        }

        //private IEnumerator FireGun()
        //{
        //    while (isFiring)
        //    {
        //        this.body.StartFiring();
        //        yield return null;
        //    }
        //}

        //private void OnPressed()
        //{
        //    isFiring = true;
        //    StartCoroutine(FireGun());
        //}

        //private void OnReleased()
        //{
        //    this.body.StopFiring();
        //    isFiring = false;
        //}
    }
}
