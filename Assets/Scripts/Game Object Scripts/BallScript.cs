using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	private float torque = 300.0F;

	private float horizontalAxis = 0.0F;
	private float verticalAxis = 0.0F;

	private Vector3 startPosition;

	private GUIText fattyEvents;

	// Use this for initialization
	void Start () 
	{
		startPosition = transform.position;
		ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager).Subscribe("Input_MoveCharacter", OnMove);

		fattyEvents = GameObject.Find("FattyEvents").GetComponent<GUIText>();
	}

	void FixedUpdate() {
		rigidbody.AddTorque(Vector3.left * torque * verticalAxis, ForceMode.Acceleration);
		rigidbody.AddTorque(Vector3.forward * torque * horizontalAxis, ForceMode.Force);
	}

	void Update()
	{
		if (transform.position.y <= -10)
		{
			transform.position = startPosition;
		}
	}
	
	void OnMove(IEventArgs eventArgs)
	{
		fattyEvents.text = "OnMove";

		MovementEventArgs movementArgs = (MovementEventArgs)eventArgs;
		horizontalAxis = movementArgs.HorizonalAxis;
		verticalAxis = movementArgs.VerticalAxis;
	}
}
