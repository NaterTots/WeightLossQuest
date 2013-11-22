using UnityEngine;
using System.Collections;

public class SodaScript : CollectibleBase 
{
	public float WeightGain = 10f;

	public override void OnPickup()
	{
		ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager).
			FireEvent(StatsManager.WeightChange, new WeightChangeEventArgs(WeightGain));
	}
}
