using UnityEngine;
using System.Collections;

public class LevelManager : IService 
{
	private LevelBase currentLevel;




	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.LevelManager;
	}

	#endregion
}
