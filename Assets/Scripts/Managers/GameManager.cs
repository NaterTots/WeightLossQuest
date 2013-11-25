using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IService
{
	public GameObject inputManager;
	public GameObject hud;

	public GameObject collectibleFactory;
	public GameObject levelManager;

	ServiceManager serviceManager;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);

		serviceManager = ServiceManager.Instance;

		if (serviceManager.ContainsService(ServiceType.EventManager))
		{
			Debug.Log("GameManager.Start being called a second time.");
		}
		else
		{
			EventManager eventManager = new EventManager();
			serviceManager.AddService(ServiceType.EventManager, eventManager);

			GameObject input = (GameObject)Instantiate(inputManager);
			input.transform.parent = this.transform;
			serviceManager.AddService(ServiceType.InputManager, input.GetComponent<InputManager>());

			GameObject hudObject = (GameObject)Instantiate(hud);
			hudObject.transform.parent = this.transform;

			StatsManager statsManager = new StatsManager();
			serviceManager.AddService(ServiceType.StatsManager, statsManager);
			//TODO: the stats manager shouldn't be responsible for initializing the stats
			statsManager.Initialize();


			GameObject collectibles = (GameObject)Instantiate (collectibleFactory);
			collectibles.transform.parent = this.transform;
			serviceManager.AddService(ServiceType.CollectibleFactory, collectibles.GetComponent<CollectibleFactory>());

			GameObject levels = (GameObject)Instantiate(levelManager);
			levels.transform.parent = this.transform;
			LevelManager levelManagerScript = levels.GetComponent<LevelManager>();
			serviceManager.AddService(ServiceType.LevelManager, levelManagerScript);

			InitializeLevels(levelManagerScript);
			//statsManager.SetWeight(1600f);
			levelManagerScript.TransitionToLevel(1);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitializeLevels(LevelManager levelManager)
	{
		//TODO: pull this from an external file
		LevelBase levelOne = new LevelBase();
		levelOne.LevelId = 1;
		levelOne.SceneName = "level1";
		levelOne.spawnFrequency = 2.0f;
		levelOne.spawnFrequencyDeviation = 1.0f;
		levelOne.WeightMax = 2200f;
		levelOne.WeightGoal = 1900f;
		levelOne.collectibles = new List<CollectibleLevelInfo>();
		levelOne.collectibles.Add(new CollectibleLevelInfo()
		{
			collectibleResourceName = "Prefabs/DumbBell",
			oddsOfCollectible = 1,
			lifetime = 15f
		});
		levelOne.collectibles.Add(new CollectibleLevelInfo()
		                          {
			collectibleResourceName = "Prefabs/SodaCan",
			oddsOfCollectible = 1,
			lifetime = 10f
		});
		levelOne.collectibles.Add(new CollectibleLevelInfo()
		                          {
			collectibleResourceName = "Prefabs/IceCreamCone",
			oddsOfCollectible = 1,
			lifetime = 10f
		});
		levelOne.collectibles.Add(new CollectibleLevelInfo()
		                          {
			collectibleResourceName = "Prefabs/Carrot",
			oddsOfCollectible = 1,
			lifetime = 10f
		});
		levelOne.collectibleSpawnLeftRange = new Vector3(-20,15,-20);
		levelOne.collectibleSpawnRightRange = new Vector3(20,15,20);

		levelManager.AddLevel(levelOne.LevelId, levelOne);

		LevelBase levelTwo = new LevelBase();
		levelTwo.LevelId = 2;
		levelTwo.SceneName = "level2";
		levelTwo.spawnFrequency = 2.0f;
		levelTwo.spawnFrequencyDeviation = 1.0f;
		levelTwo.WeightMax = 2000f;
		levelTwo.WeightGoal = 1850f;
		levelTwo.collectibles = new List<CollectibleLevelInfo>();
		levelTwo.collectibles.Add(new CollectibleLevelInfo()
		                          {
			collectibleResourceName = "Prefabs/DumbBell",
			oddsOfCollectible = 4,
			lifetime = 15f
		});
		levelTwo.collectibles.Add(new CollectibleLevelInfo()
		                          {
			collectibleResourceName = "Prefabs/SodaCan",
			oddsOfCollectible = 5,
			lifetime = 10f
		});
		levelTwo.collectibleSpawnLeftRange = new Vector3(-25,15,-25);
		levelTwo.collectibleSpawnRightRange = new Vector3(25,15,25);
		
		levelManager.AddLevel(levelTwo.LevelId, levelTwo);

		LevelBase levelThree = new LevelBase();
		levelThree.LevelId = 3;
		levelThree.SceneName = "level3";
		levelThree.spawnFrequency = 1.0f;
		levelThree.spawnFrequencyDeviation = 0.5f;
		levelThree.WeightMax = 2200f;
		levelThree.WeightGoal = 1800f;
		levelThree.collectibles = new List<CollectibleLevelInfo>();
		levelThree.collectibles.Add(new CollectibleLevelInfo()
		                          {
			collectibleResourceName = "Prefabs/DumbBell",
			oddsOfCollectible = 5,
			lifetime = 5f
		});
		levelThree.collectibles.Add(new CollectibleLevelInfo()
		                          {
			collectibleResourceName = "Prefabs/SodaCan",
			oddsOfCollectible = 5,
			lifetime = 10f
		});
		levelThree.collectibleSpawnLeftRange = new Vector3(-20,15,-6);
		levelThree.collectibleSpawnRightRange = new Vector3(20,15,18);
		
		levelManager.AddLevel(levelThree.LevelId, levelThree);
	}

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.GameManager;
	}

	#endregion
}
