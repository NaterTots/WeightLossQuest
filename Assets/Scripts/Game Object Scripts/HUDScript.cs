using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour 
{
	private GUIText weightLabelText;
	private float currentWeight;

	void Awake()
	{
		EventManager eventManager = ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager);
		eventManager.Subscribe(StatsManager.NewWeight, OnNewWeight);
	}

	// Use this for initialization
	void Start () 
	{
		GameObject weightLabel = GameObject.Find("WeightLabel");
		weightLabelText = weightLabel.GetComponent<GUIText>();

		ServiceManager.Instance.GetService<StatsManager>(ServiceType.StatsManager).Initialize();
		UpdateWeight();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnNewWeight(IEventArgs eventArgs)
	{
		NewWeightEventArgs newWeight = (NewWeightEventArgs)eventArgs;
		currentWeight = newWeight.NewWeight;
		UpdateWeight();
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
