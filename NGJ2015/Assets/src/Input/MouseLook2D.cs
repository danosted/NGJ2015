using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using Assets.src.Managers.Entities;
using UnityEngine;

namespace Assets.src.Input
{
    public class MouseLook2D : MonoBehaviour
    {
        private Transform model;
        private Transform body;
        private bool facingLeft;
        private bool _isFacingRight;
        private bool UseGamepad = false;
        public int PlayerNr;
        public Vector3 directionalVector = Vector3.zero;
        private bool isSet = false;

        public delegate void OnLookVerticalDelegate(float mag);
        public event OnLookVerticalDelegate OnLookVertical;

        public delegate void OnLookHorizontalDelegate(float mag);
        public event OnLookVerticalDelegate OnLookHorizontal;

        void Awake()
        {
            
            MouseInputHandler input = GetComponent<MouseInputHandler>();
            //input.OnPress += OnPressed;
            //input.OnRelease += OnReleased;
            body = transform.FindChild(Constants.TransformBodyName);
            StartCoroutine(PointGun());
        }

        public void Initialize(GameObject[] dependencies)
        {
            //healthBar = dependencies[0].GetComponent<HealthbarScript>();
            //healthBar.Init(health);
        }

        public void Update()
        {
            if (!isSet)
            {
                isSet = true;
                if (PlayerNr != 0)
                {
                    PlayerNr = GetComponent<Player>().gamePad;
                    UseGamepad = true;
                }
                if (PlayerNr == 0)
                {
                    UseGamepad = false;
                }
            }
            if (UseGamepad)
            {
                //Debug.Log("MouseLook2d " + directionalVector);
                if (directionalVector.x < 0)
                {
                    FaceLeft();
                }
                else
                {
                    FaceRight();
                }
            }
            if (!UseGamepad)
            {
                Debug.Log("MouseLook2d " + directionalVector);
                if (Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition).x < transform.position.x)
                {
                    FaceLeft();
                }
                else
                {
                    FaceRight();
                }
            }
            
        }

        private void FaceLeft()
        {
            if (!facingLeft)
            {
                facingLeft = true;
                float scale_x = transform.localScale.x;
                transform.localScale = new Vector3(-1f * Math.Abs(scale_x), transform.localScale.y, transform.localScale.z);
            }
        }

        private void FaceRight()
        {
            if (facingLeft)
            {
                facingLeft = false;
                float scale_x = transform.localScale.x;
                transform.localScale = new Vector3(1f * Math.Abs(scale_x), transform.localScale.y, transform.localScale.z);
            }
        }

        private IEnumerator PointGun()
        {
            while (body)
            {
                Vector3 mousepos = Vector3.zero;
                if (!UseGamepad)
                {
                    mousepos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                    Debug.DrawRay(transform.position, UnityEngine.Input.mousePosition);
                }
                else
                {
                    mousepos = (transform.position) + directionalVector * 10f;

                    Debug.DrawRay(Vector3.zero, mousepos);
                    //Debug.LogWarning("mousepos " + mousepos);
                }
                //			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
                Vector3 weaponPos = body.transform.position;
                Vector3 weaponToMouse = mousepos - transform.position;
                if (!weaponToMouse.Equals(Vector3.zero))
                {
                    float angle = Mathf.Atan(weaponToMouse.y / weaponToMouse.x);
                    if (facingLeft)
                    {
                        body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.back);
                    }
                    else
                    {
                        body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.forward);
                    }
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
