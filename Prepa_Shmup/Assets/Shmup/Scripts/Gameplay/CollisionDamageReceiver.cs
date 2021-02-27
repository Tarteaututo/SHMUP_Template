using System.Collections;
using UnityEngine;

// TODO AL : rename to emitter and the other one to receiver
/// <summary>
/// Must be used with a rigidbody on the same gameobject
/// 
/// Works with CollisionDamageEmitter
/// </summary>
[RequireComponent(typeof(Damageable))]
public class CollisionDamageReceiver : MonoBehaviour
{
	[SerializeField]
	private Damageable _selfDamageable = null;

	[SerializeField]
	private CollisionDamageEmitter _selfEmitter = null;

	private void OnTriggerEnter(Collider other)
	{
		CollisionDamageEmitter otherEmitter = other.GetComponentInParent<CollisionDamageEmitter>();
		if (otherEmitter != null)
		{
			_selfDamageable.DoDamage(otherEmitter.GetCollisionDamage());

			if (_selfEmitter != null)
			{
				otherEmitter.ApplyDamage(_selfEmitter.GetCollisionDamage());
			}

			//Debug.LogFormat("{0} {1} do {3} damage to {2} [has emitter == {4}]", 
			//	GetType().Name, 
			//	gameObject.name, 
			//	other.name, 
			//	otherEmitter.GetCollisionDamage(), 
			//	_selfEmitter != null
			//	);
		}
	}
}