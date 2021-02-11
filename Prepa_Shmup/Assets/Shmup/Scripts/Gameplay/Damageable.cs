using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private int _maxHealth = 1;

	[System.NonSerialized]
	private int _currentHealth = 0;
	#endregion Fields

	#region Events
	[System.Serializable] // currenthealth, maxhealth
	public class ReceiveDamageable_UnityEvent : UnityEvent<int, int> { }

	[System.Serializable]
	public class Damageable_UnityEvent : UnityEvent { }

	public ReceiveDamageable_UnityEvent DamageReceived = null;
	public Damageable_UnityEvent Destroyed = null;
	#endregion Events

	#region Methods
	private void OnEnable()
	{
		_currentHealth = _maxHealth;
	}

	public bool DoDamage(int damage)
	{
		_currentHealth -= damage;

		if (DamageReceived != null)
		{
			DamageReceived.Invoke(_currentHealth, _maxHealth);
		}
		if (_currentHealth <= 0)
		{
			Destroy(gameObject);
			if (Destroyed != null)
			{
				Destroyed.Invoke();
			}
		}
		return _currentHealth <= 0;
	}
	#endregion Methods

}
