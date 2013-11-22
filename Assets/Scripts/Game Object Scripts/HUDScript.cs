using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour 
{
	private GUIText weightLabelText;
	private float currentWeight;

	private GUIText AccelXLabelText;
	private GUIText AccelYLabelText;
	private GUIText AccelZLabelText;

	void Awake()
	{
		EventManager eventManager = ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager);
		eventManager.Subscribe(StatsManager.NewWeight, OnNewWeight);
		eventManager.Subscribe(InputManager.Accelerometer, OnAccelerometer);
	}

	// Use this for initialization
	void Start () 
	{
		GameObject weightLabel = GameObject.Find("WeightLabel");
		weightLabelText = weightLabel.GetComponent<GUIText>();

		GameObject accelXLabel = GameObject.Find("AccelX");
		AccelXLabelText = accelXLabel.GetComponent<GUIText>();
		
		GameObject accelYLabel = GameObject.Find("AccelY");
		AccelYLabelText = accelYLabel.GetComponent<GUIText>();
		
		GameObject accelZLabel = GameObject.Find("AccelZ");
		AccelZLabelText = accelZLabel.GetComponent<GUIText>();

		ServiceManager.Instance.GetService<StatsManager>(ServiceType.StatsManager).Initialize();
		UpdateWeight();
	}

	void OnNewWeight(IEventArgs eventArgs)
	{
		NewWeightEventArgs newWeight = (NewWeightEventArgs)eventArgs;
		currentWeight = newWeight.NewWeight;
		UpdateWeight();
	}

	void OnAccelerometer(IEventArgs eventArgs)
	{
		AccelerometerEventArgs accelEvents = (AccelerometerEventArgs)eventArgs;

		AccelXLabelText.text = "Accelerometer X: " + accelEvents.Accelerometer.x;
		AccelYLabelText.text = "Accelerometer Y: " + accelEvents.Accelerometer.y;
		AccelZLabelText.text = "Accelerometer Z: " + accelEvents.Accelerometer.z;
	}
	
	private void UpdateWeight()
	{
		weightLabelText.text = "Current Weight: " + currentWeight + " lbs";
	}
}

public class WeightChangeEventArgs : IEventArgs
{
	private float _changeAmount;
	public float ChangeAmount
	{
		get
		{
			return _changeAmount;
		}
	}

	public WeightChangeEventArgs(float changeAmount)
	{
		_changeAmount = changeAmount;
	}
}

public class NewWeightEventArgs : IEventArgs
{
	private float _newWeight;
	public float NewWeight
	{
		get
		{
			return _newWeight;
		}
	}
	
	public NewWeightEventArgs(float newWeight)
	{
		_newWeight = newWeight;
	}
}
