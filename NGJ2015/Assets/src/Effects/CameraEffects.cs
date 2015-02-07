using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Managers;
using UnityEngine;
using Random = System.Random;

namespace Assets.src.Effects
{
    public class CameraEffects : MonoBehaviour
    {
        [SerializeField]
        private float shakeTime = 1f;
        [SerializeField]
        private float shakeFreq = 1f;
        [SerializeField]
        private float magX = 1f;
        [SerializeField]
        private float magY = 1f;
        [SerializeField]
        private float magZ = 1f;
        [SerializeField]
        private float colorChangeTime = 1f;
        [SerializeField]
        private float colMag = 1f;
        [SerializeField]
        private float colChangeFreq = 1f;
        [SerializeField]
        private SpriteRenderer backgroundSprite;

        private Vector3 origin;
        private Color orig_col;

        void Start()
        {
            origin = transform.position;
            orig_col = backgroundSprite.color;
            ManagerCollection.Instance.EventManager.OnDamageTaken += OnStateChange;
            ManagerCollection.Instance.EventManager.OnPunchHit += OnStateChange;
            ManagerCollection.Instance.EventManager.OnWaveStarted += OnStateChange;
        }

        private void OnStateChange()
        {
            //if (state != 4)
            //{
                StartCameraShake();
                StartColourShow();
            //}
        }

        public void StartCameraShake()
        {
            StopCoroutine("ShakeCam");
            StartCoroutine(ShakeCam());
        }

        public void StartCameraShake(float shakeTime, float magX, float magY, float magZ)
        {
            this.shakeTime = shakeTime;
            this.magX = magX;
            this.magY = magY;
            this.magZ = magZ;
            origin = transform.position;
            StopCoroutine("ShakeCam");
            StartCoroutine(ShakeCam());
        }

        private IEnumerator ShakeCam()
        {
            float count = 0f;
            while (count < shakeTime)
            {
                float rand = UnityEngine.Random.Range(-1f, 1f);
                transform.Translate(Vector3.right * magX * rand);
                rand = UnityEngine.Random.Range(-1f, 1f);
                transform.Translate(Vector3.down * magY * rand);
                rand = UnityEngine.Random.Range(-1f, 1f);
                transform.Translate(Vector3.left * magX * rand);
                rand = UnityEngine.Random.Range(-1f, 1f);
                transform.Translate(Vector3.up * magY * rand);
                count += Time.deltaTime * shakeFreq;
                yield return new WaitForSeconds(shakeFreq * Time.deltaTime);
                transform.position = origin;
            }
        }

        public void StartColourShow()
        {
            StartCoroutine(ColourShow());
        }

        private IEnumerator ColourShow()
        {
            float count = 0f;
            while (count < colorChangeTime)
            {
                float randx = UnityEngine.Random.Range(-1f, 1f);
                float randy = UnityEngine.Random.Range(-1f, 1f);
                float randz = UnityEngine.Random.Range(-1f, 1f);
                Color col = new Color(randx, randy, randz) * colMag;
                backgroundSprite.color = col;
                count += Time.deltaTime * colChangeFreq;
                yield return new WaitForSeconds(colChangeFreq * Time.deltaTime);
                backgroundSprite.color = orig_col;
            }
        }
    }
}
