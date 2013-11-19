using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour, IService 
{
	public static string CharacterMove = "Input_MoveCharacter";
	
	public bool usePotentiometer;

	EventManager _eventManager;
	
	void Awake()
	{
		_eventManager = ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager);
		_eventManager.Register(CharacterMove);	
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
