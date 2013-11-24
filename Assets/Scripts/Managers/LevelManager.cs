using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour, IService
{
	private LevelBase currentLevel;

	private Dictionary<int, LevelBase> levelDictionary = new Dictionary<int, LevelBase>();

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

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.LevelManager;
	}

	#endregion
}
