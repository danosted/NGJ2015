using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void WaveStarted()
        {
            if (OnWaveStarted != null)
            {
                OnWaveStarted();
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
