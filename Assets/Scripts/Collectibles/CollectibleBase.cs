using UnityEngine;

public class CollectibleBase : MonoBehaviour 
{
	public delegate void PickupMethod ();

	//The particle effect that plays (looping) while the collectible is alive
	public Transform AliveParticleEffect;

	//The particle effect that plays (once) when the collectible dies (is picked up)
	public Transform DeadParticleEffect;

	void Start()
	{
		if (AliveParticleEffect != null)
		{
			AliveParticleEffect.GetComponent<ParticleSystem>().Play ();
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.name == "Fatty")
		{
			if (AliveParticleEffect != null)
			{
				AliveParticleEffect.GetComponent<ParticleSystem>().Stop();
			}

			if (DeadParticleEffect != null)
			{
				DeadParticleEffect.GetComponent<ParticleSystem>().Play();
			}

			OnPickup();
			Destroy (this.gameObject);
		}
	}

	public virtual void OnPickup()
	{
		//Nothing
	}
}
