using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class CollisionDamageEmitter : MonoBehaviour
{
	[SerializeField]
	private Damageable _damageable = null;

	[SerializeField]
	private int _collisionDamage = 1;

	[SerializeField]
	private bool _takeDamageOnCollision = false;

	public void SetTakeDamageOnCollision(bool takeDamageOnCollision)
	{
		_takeDamageOnCollision = takeDamageOnCollision;
	}

	public int GetCollisionDamage()
	{
		return _collisionDamage;
	}

	public void ApplyDamage(int damage)
	{
		if (_takeDamageOnCollision == true)
		{
			_damageable.DoDamage(damage);
		}
	}
}
