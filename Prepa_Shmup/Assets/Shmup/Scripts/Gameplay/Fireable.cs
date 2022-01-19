using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour
{
	[SerializeField]
	private Projectile _projectilePrefab = null;

	private bool _canFire = false;

	public void SetCanFire(bool canFire)
	{
		_canFire = canFire;
	}

	public void DoFire()
	{
		if (_canFire == true)
		{
			var instance = Instantiate(_projectilePrefab);
			instance.transform.position = transform.position;
			instance.transform.rotation = transform.rotation;
		}
	}

	public void DoFire(Quaternion rotation)
	{
		if (_canFire == true)
		{
			var instance = Instantiate(_projectilePrefab);
			instance.transform.position = transform.position;
			instance.transform.rotation = rotation;
		}
	}
}
