using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherEmitter : MonoBehaviour
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
			ProjectileLauncher fireable = other.GetComponentInParent<ProjectileLauncher>();
			if (fireable != null)
			{
				fireable.LaunchProjectile(transform.rotation);
			}
		}
	}
}
