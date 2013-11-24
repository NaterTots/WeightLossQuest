using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBase 
{

	public float WeightGoal;
	public float WeightMax;

	public string SceneName;
	public int LevelId;

	//These vectors describe where the collectibles should spawn
	//Although they say 'left' and 'right', the collectibles will just spawn between them
	public Vector3 collectibleSpawnLeftRange;
	public Vector3 collectibleSpawnRightRange;

	//These values (all in seconds) describe the frequency with which collectibles drop,
	//their lifetimes, and the deviation from the frequency that they
	//will drop
	//Ex: 
	//    spawnFrequency = 2.0f
	//    spawnFrequencyDeviation = 0.75f
	//  - This means that collectibles will spawn every 1.25 to 2.75 seconds, computed randomly
	//			on a per-collectible instance basis (to simulate randomness)
	public float spawnFrequency;
	public float spawnFrequencyDeviation;
	
	public List<CollectibleLevelInfo> collectibles;

	private float collectibleTimer;
	private CollectibleFactory collectibleFactory;

	// Use this for initialization
	public void OnStartLevel() 
	{
		Application.LoadLevel (SceneName);

		collectibleFactory = ServiceManager.Instance.GetService<CollectibleFactory>(ServiceType.CollectibleFactory);
		ResetCollectibleTimer();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		collectibleTimer -= Time.deltaTime;

		if (collectibleTimer <= 0f)
		{
			//TODO: does the level need a reference to this?
			collectibleFactory.CreateNewCollectible(collectibleSpawnLeftRange, collectibleSpawnRightRange, collectibles);

			ResetCollectibleTimer();
		}
	}

	public void OnEndLevel()
	{

	}

	void ResetCollectibleTimer()
	{
		collectibleTimer = Random.Range(spawnFrequency - spawnFrequencyDeviation, spawnFrequency + spawnFrequencyDeviation);
	}
}


