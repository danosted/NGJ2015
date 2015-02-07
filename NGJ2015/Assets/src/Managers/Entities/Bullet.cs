using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 5.0f;
	private Vector3 dir;

	// Use this for initialization
	void Start () {
		Vector3 mousepos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
		//			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
		Vector3 weaponPos = transform.position;
		dir = (mousepos - weaponPos).normalized;
	}
	
	void Update() {
		transform.position += dir * speed * Time.deltaTime;
	}
}
