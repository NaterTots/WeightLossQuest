using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Transform target;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Fatty").transform;
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {

		float x = Mathf.Lerp(transform.position.x, target.position.x + offset.x, 200f);
		float y = transform.position.y;
		//float y = Mathf.Lerp(transform.position.y, target.position.y + offset.y, 200f);
		float z = Mathf.Lerp(transform.position.z, target.position.z + offset.z, 200f);

		transform.position = new Vector3(x, y, z);
	}
}