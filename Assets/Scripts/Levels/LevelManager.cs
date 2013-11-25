using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour, IService
{
	public static string NewTargetWeightEvent = "NewTargetWeight";

	private LevelBase currentLevel;

	private Dictionary<int, LevelBase> levelDictionary = new Dictionary<int, LevelBase>();

	private EventManager eventManager;

	public void Awake()
	{
		eventManager = ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager);
		eventManager.Subscribe(StatsManager.NewWeight, OnNewWeight);
		eventManager.Register(NewTargetWeightEvent);
	}

	public void AddLevel(int id, LevelBase level)
	{
		if (!levelDictionary.ContainsKey(id))
		{
			levelDictionary.Add(id, level);
		}
		else
		{
			Debug.LogWarning("Attempting to add multiple levels with the same Id: " + id.ToString());
		}
	}

	public void TransitionToNextLevel()
	{
		TransitionToLevel(currentLevel.LevelId + 1);
	}

	public void TransitionToLevel(int id)
	{
		if (levelDictionary.ContainsKey(id))
		{
			if (currentLevel != null)
			{
				currentLevel.OnEndLevel();
			}
			currentLevel = levelDictionary[id];
			currentLevel.OnStartLevel();

			eventManager.FireEvent(NewTargetWeightEvent, new NewTargetWeightEventArgs(currentLevel.WeightGoal, currentLevel.WeightMax));
		}
		else
		{
			Debug.LogError("Unable to find level with Id: " + id.ToString());
		}
	}

	public void Update()
	{
		if (currentLevel != null)
		{
			currentLevel.Update();
		}
	}

	public void OnNewWeight(IEventArgs args)
	{
		if (currentLevel == null) return;

		NewWeightEventArgs newWeightArgs = (NewWeightEventArgs)args;
		
		if (newWeightArgs.NewWeight <= currentLevel.WeightGoal)
		{
			//Beat the level!
			//TODO: do more than this
			TransitionToNextLevel();
		}
		else if (newWeightArgs.NewWeight >= currentLevel.WeightMax)
		{
			//Lost the level!
			ServiceManager.Instance.GetService<StatsManager>(ServiceType.StatsManager).SetWeight(2000f);
			//TODO: do more than this
			TransitionToLevel(1);
		}
	}

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.LevelManager;
	}

	#endregion
}

public class NewTargetWeightEventArgs : IEventArgs
{
	private float _goalWeight;
	public float GoalWeight
	{
		get
		{
			return _goalWeight;
		}
	}

	private float _maxWeight;
	public float MaxWeight
	{
		get
		{
			return _maxWeight;
		}
	}

	public NewTargetWeightEventArgs(float goalWeight, float maxWeight)
	{
		_goalWeight = goalWeight;
		_maxWeight = maxWeight;
	}
}