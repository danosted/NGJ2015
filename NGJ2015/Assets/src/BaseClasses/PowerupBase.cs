using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.BaseClasses
{
    public class PowerupBase : MonoBehaviour
    {
        [SerializeField]
        private float _speedMultiplier;

        public float SpeedMultiplier
        {
            get { return _speedMultiplier; }
            set { _speedMultiplier = value; }
        }

        [SerializeField]
        private float _damageMultiplier;

        public float DamageMultiplier
        {
            get { return _damageMultiplier; }
            set { _damageMultiplier = value; }
        }

        public void Initialize(float? speedMultiplier, float? damageMultiplier)
        {
            _speedMultiplier = speedMultiplier != null ? speedMultiplier.Value : 0f;
            _damageMultiplier = damageMultiplier != null ? damageMultiplier.Value : 0f;
        }
    }
}
