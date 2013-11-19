using UnityEngine;
using System.Collections;

public class BurgerScript : MonoBehaviour {

	public float WeightGain = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.gameObject.name == "Fatty")
		{
			ServiceManager.Instance.GetService<EventManager>(ServiceType.EventManager).
				FireEvent(HUDScript.WeightChange, new WeightChangeEventArgs(WeightGain));

			Destroy(this.gameObject);
		}
	}
}
