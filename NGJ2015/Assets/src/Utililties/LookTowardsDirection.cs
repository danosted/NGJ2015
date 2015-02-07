using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using Assets.src.Managers.Entities;
using UnityEngine;

namespace Assets.src.Utililties
{
    public class LookTowardsDirection : MonoBehaviour
    {
        private Transform model;
        private Transform body;
        private bool facingLeft;
        private bool _isFacingRight;
        private Enemy entity;

        void Awake()
        {
            entity = GetComponent<Enemy>();
            //Debug.LogWarning(entity);
            body = transform.FindChild(Constants.TransformBodyName);
            //Debug.LogWarning(transform.GetChild(0).childCount);
            //Debug.LogWarning(body);
        }

        void Start()
        {
            StartCoroutine(PointGun());
        }

        void Update()
        {
            if (Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition).x < transform.position.x)
            {
                FaceLeft();
            }
            else
            {
                FaceRight();
            }
        }

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
            while (entity)
            {
                //Vector3 pos = body.transform.position;
                //Vector3 weaponToMouse = (entity.Direction - pos).normalized;
                
                //float angle = Mathf.Atan(weaponToMouse.y / weaponToMouse.x);
                //if (facingLeft)
                //{
                //    body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.back);
                //}
                //else
                //{
                //    body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.forward);
                //}
                yield return null;
            }
        }
    }
}
