using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour 
{
	private GUIText weightLabelText;
	private GUITexture weightGraphicTexture;
	private float currentWeight = 0f;
	private float goalWeight;
	private float maxWeight;

	private GUIText AccelXLabelText;
	private GUIText AccelYLabelText;
	private GUIText AccelZLabelText;

	private bool hasStarted = false;

	void Awake()
	{
		EventManager eventManager = ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager);
		eventManager.Subscribe(StatsManager.NewWeight, OnNewWeight);
		eventManager.Subscribe(InputManager.Accelerometer, OnAccelerometer);
		eventManager.Subscribe(LevelManager.NewTargetWeightEvent, OnNewWeightGoal);
	}

	// Use this for initialization
	void Start () 
	{
		hasStarted = true;

		GameObject weightLabel = GameObject.Find("WeightLabel");
		weightLabelText = weightLabel.GetComponent<GUIText>();

		GameObject weightGraphic = GameObject.Find ("WeightGraphic");
		weightGraphicTexture = weightGraphic.GetComponent<GUITexture>();

		GameObject accelXLabel = GameObject.Find("AccelX");
		AccelXLabelText = accelXLabel.GetComponent<GUIText>();
		
		GameObject accelYLabel = GameObject.Find("AccelY");
		AccelYLabelText = accelYLabel.GetComponent<GUIText>();
		
		GameObject accelZLabel = GameObject.Find("AccelZ");
		AccelZLabelText = accelZLabel.GetComponent<GUIText>();
	
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

	void OnNewWeightGoal(IEventArgs eventArgs)
	{
		NewTargetWeightEventArgs targetWeightArgs = (NewTargetWeightEventArgs)eventArgs;
		goalWeight = targetWeightArgs.GoalWeight;
		maxWeight = targetWeightArgs.MaxWeight;
		UpdateWeight();
	}

	private void UpdateWeight()
	{
		Debug.Log("Goal Weight: " + goalWeight.ToString() + " Max: " + maxWeight.ToString() + " Current: " + currentWeight.ToString());

		if (!hasStarted) return;

		weightLabelText.text = currentWeight + " LBS";

		//unknown behavior if these aren't all positive
		if (goalWeight > 0 && maxWeight > 0 && currentWeight > 0)
		{
			//The texture's size ranges from 75 to 125, which is a 50-pixel difference
			float pixelsPerPound = 50f / (maxWeight - goalWeight);
			float newTextureDimensions = 75 + pixelsPerPound * (currentWeight - goalWeight);

			//now reset the location so it remains centered
			//the midpoint is at 0, when the dimensions are 100
			//at the extremes, the location is -12.5 or 12.5
			float newTexturePosition = (100f - newTextureDimensions) / 2f;

			weightGraphicTexture.pixelInset = new Rect(newTexturePosition, newTexturePosition, newTextureDimensions, newTextureDimensions);
			Debug.Log("Fatty X,Y: " + newTexturePosition.ToString() + " Fatty Size: " + newTextureDimensions.ToString());
		}
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
