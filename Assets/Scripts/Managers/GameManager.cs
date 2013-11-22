using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour, IService
{
	public GameObject inputManager;
	public GameObject hud;

	ServiceManager serviceManager;

	// Use this for initialization
	void Awake () 
	{
		serviceManager = ServiceManager.Instance;

		EventManager eventManager = new EventManager();
		serviceManager.AddService(ServiceType.EventManager, eventManager);

		GameObject input = (GameObject)Instantiate(inputManager);
		input.transform.parent = this.transform;

		serviceManager.AddService(ServiceType.InputManager, input.GetComponent<InputManager>());

		serviceManager.AddService(ServiceType.StatsManager, new StatsManager());
		serviceManager.AddService(ServiceType.CollectibleFactory, new CollectibleFactory());
		serviceManager.AddService(ServiceType.LevelManager, new LevelManager());

		GameObject hudObject = (GameObject)Instantiate(hud);
		hudObject.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.GameManager;
	}

	#endregion
}
