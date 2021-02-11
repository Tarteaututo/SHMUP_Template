using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private float _moveSpeed = 1f;

	[SerializeField]
	private int _damage = 1;

	[SerializeField]
	private float _selfDestructDelay = 10f;
	#endregion Fields

	#region Events
	[System.Serializable]
	public class Projectile_UnityEvent : UnityEvent<Damageable> { }
	public Projectile_UnityEvent DestroyTarget = null;
	#endregion Events

	#region Methods
	private void OnEnable()
	{
		StartCoroutine(WaitForSelfDestruct());
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		transform.position += Time.deltaTime * _moveSpeed * transform.forward;
	}

	private void OnTriggerEnter(Collider other)
	{
		Damageable damageable = other.GetComponentInParent<Damageable>();

		if (damageable != null)
		{
			bool isDestroyed = damageable.DoDamage(_damage);
			if (isDestroyed == true && DestroyTarget != null)
			{
				DestroyTarget.Invoke(damageable);
			}

			Destroy(gameObject);
		}
	}

	private IEnumerator WaitForSelfDestruct()
	{
		yield return new WaitForSeconds(_selfDestructDelay);
		if (gameObject)
		{
			Destroy(gameObject);
		}
	}
	#endregion Methods

}
