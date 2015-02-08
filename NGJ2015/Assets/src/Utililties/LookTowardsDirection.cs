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
        [SerializeField]
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
            if (entity.TargetPosition.x < transform.position.x)
            {
                FaceLeft();
            }
            else
            {
                FaceRight();
            }
            Vector3 pos = transform.position;
            Vector3 direction = (entity.TargetPosition - pos).normalized;
            Debug.DrawRay(transform.position, direction * 10f);
            //Debug.DrawRay(transform.position, Vector3.left * 10f, Color.red);
            //Debug.DrawRay(transform.position, Vector3.right * 10f, Color.blue);
            float angle = Mathf.Atan(direction.y / direction.x);
            if (!body || angle.Equals(float.NaN)) return;
            if (facingLeft)
            {
                body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.back);
            }
            else
            {
                body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.forward);
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
            //while (body && entity)
            //{
            //    Vector3 pos = transform.position;
            //    Vector3 direction = (entity.TargetPosition - pos).normalized;
            //    Debug.DrawRay(transform.position, direction * 10f);
            //    //Debug.DrawRay(transform.position, Vector3.left * 10f, Color.red);
            //    //Debug.DrawRay(transform.position, Vector3.right * 10f, Color.blue);
            //    float angle = Mathf.Atan(direction.y / direction.x);
            //    if (facingLeft)
            //    {
            //        body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.back);
            //    }
            //    else
            //    {
            //        body.transform.rotation = Quaternion.AngleAxis(angle * 180f / Mathf.PI, Vector3.forward);
            //    }
            //    yield return null;
            //}
            yield return null;
        }
    }
}
