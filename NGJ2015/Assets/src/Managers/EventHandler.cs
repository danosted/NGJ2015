using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers
{
    public class EventHandler
    {
        public delegate void OnDamageTakenDelegate();
        public event OnDamageTakenDelegate OnDamageTaken;

        public delegate void OnPunchHitDelegate();
        public event OnPunchHitDelegate OnPunchHit;

        public delegate void OnPunchMissDelegate();
        public event OnPunchMissDelegate OnPunchMiss;

        public delegate void OnWaveStartedDelegate();
        public event OnWaveStartedDelegate OnWaveStarted;

        public delegate void OnGameStartedDelegate();
        public event OnGameStartedDelegate OnGameStarted;

        public void WaveStarted()
        {
            if (OnWaveStarted != null)
            {
                OnWaveStarted();
            }
        }

        public void GameStarted()
        {
            if (OnGameStarted != null)
            {
                OnGameStarted();
            }
        }

        public void PunchHit()
        {
            if (OnPunchHit != null)
            {
                OnPunchHit();
            }
        }

        public void PunchMiss()
        {
            if (OnPunchMiss != null)
            {
                OnPunchMiss();
            }
        }
    }
}
