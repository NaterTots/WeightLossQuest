using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour, IService 
{
	public static string CharacterMove = "Input_MoveCharacter";
	public static string Accelerometer = "Input_Accelerometer";


	public bool usePotentiometer;

	EventManager _eventManager;
	
	void Awake()
	{
		_eventManager = ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager);
		_eventManager.Register(CharacterMove);
		_eventManager.Register(Accelerometer);
	}
	
	// Use this for initialization
	void Start () 
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			usePotentiometer = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal = 0.0f; 
		float vertical = 0.0f; 
		
		if (usePotentiometer)
		{
			horizontal = Input.acceleration.x;
			vertical = Input.acceleration.y;

			_eventManager.FireEvent(Accelerometer, new AccelerometerEventArgs(Input.acceleration));
		}
		else
		{
			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");
		}
		
		_eventManager.FireEvent(CharacterMove, new MovementEventArgs(horizontal, vertical));
    }

	#region IService implementation
	
	public ServiceType GetServiceType ()
	{
		return ServiceType.InputManager;
	}
	#endregion
}

public class MovementEventArgs : IEventArgs
{
	private float _horizontalAxis;
	public float HorizonalAxis
	{
		get
		{
			return _horizontalAxis;	
		}
	}

	private float _verticalAxis;
	public float VerticalAxis
	{
		get
		{
			return _verticalAxis;	
		}
	}
	
	public MovementEventArgs(float horizontal, float vertical)
	{
		_horizontalAxis = horizontal;
		_verticalAxis = vertical;
	}
}

public class AccelerometerEventArgs : IEventArgs
{
	private Vector3 _accelerometer;
	public Vector3 Accelerometer
	{
		get
		{
			return _accelerometer;
		}
	}

	public AccelerometerEventArgs(Vector3 accel)
	{
		_accelerometer = accel;
	}
}
