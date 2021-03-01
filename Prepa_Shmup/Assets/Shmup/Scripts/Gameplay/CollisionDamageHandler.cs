using System.Collections;
using UnityEngine;

/// <summary>
/// Must be used with a rigidbody on the same gameobject
/// 
/// </summary>
[RequireComponent(typeof(Damageable))]
public class CollisionDamageHandler : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private Damageable _selfDamageable = null;

	[SerializeField]
	private int _collisionDamage = 1;

	[SerializeField]
	private bool _takeDamageOnCollision = false;

	[SerializeField]
	private bool _displayLogs = false;
	#endregion Fields

	#region Methods
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
			_selfDamageable.DoDamage(damage);
		}
	}

	public void TryApplyCollisionDamage(CollisionDamageHandler otherColliderDamageHandler)
	{
		if (otherColliderDamageHandler != null)
		{
			if (_takeDamageOnCollision == true)
			{
				_selfDamageable.DoDamage(otherColliderDamageHandler.GetCollisionDamage());
			}
			otherColliderDamageHandler.ApplyDamage(GetCollisionDamage());

			if (_displayLogs == true)
			{
				Debug.LogFormat(
					"{0} {1} do {3} damage to {2}",
					GetType().Name,
					gameObject.name,
					otherColliderDamageHandler.name,
					otherColliderDamageHandler.GetCollisionDamage()
				);
			}
		}
	}

	public void TryApplyCollisionDamage(ATriggerDispatcher sender, ATriggerDispatcher.TriggerDispatcherEventArgs args)
	{
		TryApplyCollisionDamage(args.other.GetComponentInParent<CollisionDamageHandler>());
	}
	#endregion Methods

}