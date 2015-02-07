using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SwarmScript : MonoBehaviour {
	
	private List<GameObject> jews = new List<GameObject>();
	private Vector3 playerPosition;
	private bool jewsCollision = false;
	public Vector3 tendencyPlace;
	
    //public ObjectsManagerScript obms;
	
	public int jewsCounter;
	
	// Jew movement variables
	private float jewRadius;
	public float maxJewSpeed = 0.5f;
	public float minJewDistance = 0.5f;
	public float moveFrequency = 100f;
	
	void Start () {
//		GameObject[] j = GameObject.FindGameObjectsWithTag("Jew");
//		foreach (GameObject x in j) jews.Add(x);
		jewsCounter = 0;
	}
	
	
	// Update is called once per frame
	void Update () {
		setTendencyPlace();
		moveJews();
//		if(jewsCollision)
//		{
//			jewsCollision = false;
//			jewsCounter++;
//			Debug.Log ("jews:"+ jewsCounter);
//		}
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Jew")
		{
			
			collision.gameObject.transform.parent = null;
			jews.Add(collision.gameObject);
			
			//putInSwarm(collision.gameObject.GetComponent<Jew>() as Jew);
            //jews[jews.Count-1].GetComponent<Jew>().setActive(true);
			
//			Vector3 forceDir = (jews[jews.Count-1].GetComponent<Jew>().getPosition().x >= 0)? Vector3.left: Vector3.right; 
//			jews[jews.Count-1].rigidbody.AddForce(5f * forceDir, ForceMode.Impulse);
			
			Debug.Log ("jew added!");
			//jewsCounter++;
			//jewsCollision = true;
			
			Debug.Log (jewsCounter);
            //if(obms.jews1.Contains(collision.gameObject)) {
            //    obms.jews1.Remove(collision.gameObject);
            //} else {
            //    obms.jews2.Remove(collision.gameObject);
            //}
		}
	}
	
	void setTendencyPlace() {
		playerPosition = gameObject.transform.position;
		tendencyPlace = new Vector3(playerPosition.x, 0.25f, playerPosition.z - 2);
	}
	
	void moveJews () {
		
		Vector3 jewVelocity;
		foreach (var jew in jews) {
			jewVelocity = jew.rigidbody.velocity;
			// Setting the velocity of the jew
			if (jews.Count > 1){
				jewVelocity += gatherJews(jew);
				jewVelocity += keepJewDistance(jew);
			}
			jewVelocity += tendToPlace(jew);
				
			// keeping the y axis velocity to zero
			jewVelocity = new Vector3 (jewVelocity.x, 0, jewVelocity.z);
			// Limiting the speed of the jews.
			if ((jewVelocity.magnitude!= 0) && jewVelocity.magnitude > maxJewSpeed) {
				jewVelocity *= (maxJewSpeed / jewVelocity.magnitude);
			}
			
			// Moving the jew according to the velocity
			
			jew.transform.position = jew.transform.position + jewVelocity;
			jew.transform.position = new Vector3(jew.transform.position.x, 0.25f, jew.transform.position.z);
	
		}
	}
	
	Vector3 gatherJews (GameObject jew) {
		// Variable for the percieved center of the other jews.
		Vector3 percievedCenter = new Vector3 (0, 0, 0);
		
		foreach (var otherJew in jews) {
			
				if (otherJew.GetInstanceID() != jew.GetInstanceID()) {
					percievedCenter += otherJew.transform.position;
				}
				percievedCenter /= jews.Count-1;
		}
		
		// Return the vector from the jew to the percieved center
		return (percievedCenter - jew.transform.position)/moveFrequency; 
	}
	
	Vector3 keepJewDistance (GameObject jew) {
		Vector3 targetPosition = new Vector3(0, 0, 0);
		Vector3 jewDistance;
		foreach (var otherJew in jews) {
			jewDistance = otherJew.transform.position - jew.transform.position;
			if ((jew.GetInstanceID() != otherJew.GetInstanceID()) && (jewDistance.magnitude < minJewDistance))
				targetPosition -= jewDistance*(minJewDistance-jewDistance.magnitude);
		}
		return targetPosition;
	}
	
	Vector3 tendToPlace(GameObject jew) {
		return (tendencyPlace - jew.transform.position) * (tendencyPlace - jew.transform.position).magnitude / (moveFrequency);
	}
	
	public void removeJew(float percent) {
		int jewnum = (int)percent*(jews.Count-1);
		if(jews.Count >= jewnum) {
			for(int i = 0; i < jewnum; i++) {
				GameObject temp = jews[jews.Count-1];
				jews.RemoveAt(jews.Count-1);
				Destroy(temp);
			}
		} else {
			foreach(GameObject go in jews) {
				Destroy(go);
			}
			jews.Clear();		
		}
		
	}
}
