using UnityEngine;
using System.Collections;

public class IntroLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel(1);
		}
        if (Input.GetMouseButtonUp(0))
        {
            Application.LoadLevel(1);
        }
	}

}
