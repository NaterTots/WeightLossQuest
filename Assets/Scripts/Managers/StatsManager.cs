using UnityEngine;
using System.Collections;

public class StatsManager : IService {

	public float CurrentWeight = 2000F;

	private EventManager eventManager;

	// Use this for initialization
	public StatsManager (EventManager eventMan) 
	{
		eventManager = eventMan;
	}

	public void Initialize()
	{
		eventManager.Subscribe(HUDScript.WeightChange, OnWeightChange);

		eventManager.FireEvent(HUDScript.NewWeight, new NewWeightEventArgs(CurrentWeight));
	}

	void OnWeightChange(IEventArgs args)
	{
		WeightChangeEventArgs weightChangeArgs = (WeightChangeEventArgs)args;
		CurrentWeight += weightChangeArgs.ChangeAmount;
		if (CurrentWeight <= 0f)
		{
			//Probably fire an event here?
			CurrentWeight = 0f;
		}

		eventManager.FireEvent(HUDScript.NewWeight, new NewWeightEventArgs(CurrentWeight));
	}

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.StatsManager;
	}

	#endregion
}
