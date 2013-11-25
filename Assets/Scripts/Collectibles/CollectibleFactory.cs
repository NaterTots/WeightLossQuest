using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleFactory : MonoBehaviour, IService
{
	public GameObject CreateNewCollectible(Vector3 spawnLeft, Vector3 spawnRight, List<CollectibleLevelInfo> possibleCollectibles)
	{
		GameObject createdCollectible = null;

		//First, find a suitable location
		bool foundSpawnLocation = false;
		Vector3 spawnLocation = new Vector3(0,0,0);
		do
		{
			Vector3 tempLoc = new Vector3(
				Random.Range (spawnLeft.x > spawnRight.x ? spawnRight.x : spawnLeft.x, spawnLeft.x > spawnRight.x ? spawnLeft.x : spawnRight.x),
				Random.Range (spawnLeft.y > spawnRight.y ? spawnRight.y : spawnLeft.y, spawnLeft.y > spawnRight.y ? spawnLeft.y : spawnRight.y),
				Random.Range (spawnLeft.z > spawnRight.z ? spawnRight.z : spawnLeft.z, spawnLeft.z > spawnRight.z ? spawnLeft.z : spawnRight.z));

			//draw a ray directly down (the path the collectible will follow when it falls)
			//if the ray hits something, then we'll allow the collectible to be dropped at this location
			//Note: this avoids collectibles falling into holes and gaps throughout the level
			if (Physics.Raycast(tempLoc, new Vector3(0, -100, 0)))
			{
				foundSpawnLocation = true;
				spawnLocation = tempLoc;
			}
		} while (foundSpawnLocation == false); //TODO: do we want to have an arbitrary breakout point here? how to handle this logic not finding a suitable drop location?

		int totalOdds = 0;
		foreach (CollectibleLevelInfo collectible in possibleCollectibles) 
		{
			totalOdds += collectible.oddsOfCollectible;
		}

		int collectibleRange = Random.Range (0, totalOdds);
		for (int i = 0; i < possibleCollectibles.Count; i++) 
		{
			collectibleRange -= possibleCollectibles[i].oddsOfCollectible;
			if (collectibleRange < 0 || i == possibleCollectibles.Count - 1)
			{
				createdCollectible = (GameObject)Instantiate(Resources.Load (possibleCollectibles[i].collectibleResourceName), spawnLocation, Quaternion.identity);
				createdCollectible.SendMessage("SetLifetime", possibleCollectibles[i].lifetime);
				break;
			}
		}

		return createdCollectible;
	}

	#region IService implementation

	public ServiceType GetServiceType ()
	{
		return ServiceType.CollectibleFactory;
	}

	#endregion
}

public struct CollectibleLevelInfo
{
	public string collectibleResourceName;
	public int oddsOfCollectible;
	public float lifetime; //in seconds
}