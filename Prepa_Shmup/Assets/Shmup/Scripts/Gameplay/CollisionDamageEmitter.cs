using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageEmitter : MonoBehaviour
{
	[SerializeField]
	private int _collisionDamage = 1;

	[System.NonSerialized]
	private bool _isDestroyedByCollision = false;

	public bool IsDestroyedByCollision()
	{
		return _isDestroyedByCollision;
	}

	public void SetIsDestroyedByCollision(bool isDestroyedByCollision)
	{
		_isDestroyedByCollision = isDestroyedByCollision;
	}

	public int GetCollisionDamage()
	{
		return _collisionDamage;
	}
}
