using UnityEngine;

public class CollectibleBase : MonoBehaviour 
{
	public delegate void PickupMethod ();

	//The particle effect that plays (looping) while the collectible is alive
	public Transform AliveParticleEffect;

	//The particle effect that plays (once) when the collectible dies (is picked up)
	public Transform DeadParticleEffect;

	//The mesh, rigid body, and collider corresponding to the collectible
	public Transform CollectibleMesh;

	//A method that gets called when the collectible is picked up
	public PickupMethod PickupEffect;

	void OnCollisionEnter(Collision coll)
	{
		//TODO:figure out this
		if (...)
		{
			DeadParticleEffect.GetComponent<ParticleEffect>().Play();
			PickupEffect();
			Destroy(this.gameObject);
		}
	}
}
