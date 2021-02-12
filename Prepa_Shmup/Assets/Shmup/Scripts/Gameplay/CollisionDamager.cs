using System.Collections;
using UnityEngine;

/// <summary>
/// Must be used with a rigidbody on the same gameobject
/// </summary>
public class CollisionDamager : MonoBehaviour
{
	[SerializeField]
	private Damageable _damageable = null;

	[SerializeField]
	private bool _destroyOther = false;

	private void OnTriggerEnter(Collider other)
	{
		Actor actor = other.GetComponentInParent<Actor>();
		if (actor != null)
		{
			_damageable.DoDamage(actor.GetCollisionDamage());
			if (_destroyOther == true)
			{
				Damageable actorDamageable = actor.GetComponent<Damageable>();
				if (actorDamageable != null)
				{
					actorDamageable.DoDamage(1000);
				}
			}
		}
	}
}