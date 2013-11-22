using UnityEngine;
using System.Collections;

public class CollectibleFactory : IService 
{
	
	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.CollectibleFactory;
	}

	#endregion
}
