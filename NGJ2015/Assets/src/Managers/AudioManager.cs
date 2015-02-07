using System.Collections.Generic;
using System.Linq;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Managers
{
    public class AudioManager : MonoBehaviour
    {

		public AudioClip[] audioClips;

        public void PlayAudio(Enumerations.Audio audioName)
        {
			var audioToPlay = audioClips.Where (a => a.name == audioName.ToString ()).SingleOrDefault();
			if (audioToPlay != null) {
				AudioSource.PlayClipAtPoint (audioToPlay, new Vector3 (0, 0, 0));
			} else {
				Debug.LogError("Audio was not found");
			}
		}
    }
}
