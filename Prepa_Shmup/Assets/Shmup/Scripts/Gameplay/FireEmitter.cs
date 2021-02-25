using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEmitter : MonoBehaviour
{
	[SerializeField]
	private float _delay = 0f;

	[System.NonSerialized]
	private float _currentTime = 0;

	private void OnEnable()
	{
		_currentTime = 0;
	}

	private void LateUpdate()
	{
		_currentTime -= Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_currentTime <= 0)
		{
			_currentTime = _delay;
			var fireable = other.GetComponentInParent<Fireable>();
			if (fireable != null)
			{
				fireable.DoFire(transform.rotation);
			}
		}
	}
}
