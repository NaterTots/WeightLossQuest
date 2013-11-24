using UnityEngine;

public class CollectibleBase : MonoBehaviour 
{
	public delegate void PickupMethod ();

	//The particle effect that plays (looping) while the collectible is alive
	public Transform AliveParticleEffect;

	//The particle effect that plays (once) when the collectible dies (is picked up)
	public Transform DeadParticleEffect;

	private float lifeTimer;
	private float collectibleLifetime;

	void Start()
	{
		lifeTimer = 0f;

		if (AliveParticleEffect != null)
		{
			AliveParticleEffect.GetComponent<ParticleSystem>().Play ();
		}
	}

	void Update()
	{
		lifeTimer += Time.deltaTime;
		if (lifeTimer > collectibleLifetime)
		{
			//TODO: add a particle effect that shows it evaporating away
			Destroy (this.gameObject);
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

	public void SetLifetime(float lifetime)
	{
		collectibleLifetime = lifetime;
	}

	public virtual void OnPickup()
	{
		//Nothing
	}
}
