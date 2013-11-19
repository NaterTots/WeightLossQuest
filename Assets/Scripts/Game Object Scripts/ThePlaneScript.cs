using UnityEngine;
using System.Collections;

public class ThePlaneScript : MonoBehaviour {

	public int tiltAngle = 30;
	public float smooth = 2.0F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Horizontal axis is -1 to 1
		//Corresponds to rotation of -30 to +30
		/*float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * -tiltAngle;
		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
		*/
	}
}
