using UnityEngine;
using System.Collections;

public class StatsManager : IService 
{
	public static string WeightChange = "WeightChange";
	public static string NewWeight = "NewWeight";

	public float CurrentWeight = 2000F;

	private EventManager eventManager;

	// Use this for initialization
	public StatsManager () 
	{
		eventManager = ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager);
		eventManager.Register(WeightChange);
		eventManager.Register(NewWeight);

		eventManager.Subscribe(WeightChange, OnWeightChange);
	}

	public void Initialize()
	{		
		eventManager.FireEvent(NewWeight, new NewWeightEventArgs(CurrentWeight));
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

		eventManager.FireEvent(NewWeight, new NewWeightEventArgs(CurrentWeight));
	}

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.StatsManager;
	}

	#endregion
}
