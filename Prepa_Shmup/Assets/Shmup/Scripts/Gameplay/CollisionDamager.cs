using System.Collections;
using UnityEngine;

// TODO AL : we must add a collision damager emiitter class and get rid of actor dependency

/// <summary>
/// Must be used with a rigidbody on the same gameobject
/// 
/// Works with CollisionDamageEmitter
/// </summary>
public class CollisionDamager : MonoBehaviour
{
	[SerializeField]
	private Damageable _damageable = null;

	private void OnTriggerEnter(Collider other)
	{
		CollisionDamageEmitter emitter = other.GetComponentInParent<CollisionDamageEmitter>();
		if (emitter != null)
		{
			_damageable.DoDamage(emitter.GetCollisionDamage());

			if (emitter.IsDestroyedByCollision() == true)
			{
				Damageable actorDamageable = emitter.GetComponent<Damageable>();
				if (actorDamageable != null)
				{
					actorDamageable.DoDamage(1000);
				}
			}
		}
	}
}