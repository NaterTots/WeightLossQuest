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

			serviceManager.AddService(ServiceType.StatsManager, new StatsManager());

			GameObject collectibles = (GameObject)Instantiate (collectibleFactory);
			collectibles.transform.parent = this.transform;
			serviceManager.AddService(ServiceType.CollectibleFactory, collectibles.GetComponent<CollectibleFactory>());

			GameObject levels = (GameObject)Instantiate(levelManager);
			levels.transform.parent = this.transform;
			LevelManager levelManagerScript = levels.GetComponent<LevelManager>();
			serviceManager.AddService(ServiceType.LevelManager, levelManagerScript);

			GameObject hudObject = (GameObject)Instantiate(hud);
			hudObject.transform.parent = this.transform;

			InitializeLevels(levelManagerScript);
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
		levelOne.spawnFrequency = 4.0f;
		levelOne.spawnFrequencyDeviation = 1.0f;
		levelOne.WeightMax = 2200f;
		levelOne.WeightGoal = 1800f;
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
		levelOne.collectibleSpawnLeftRange = new Vector3(-20,15,-20);
		levelOne.collectibleSpawnRightRange = new Vector3(20,15,20);

		levelManager.AddLevel(levelOne.LevelId, levelOne);
	}

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.GameManager;
	}

	#endregion
}
