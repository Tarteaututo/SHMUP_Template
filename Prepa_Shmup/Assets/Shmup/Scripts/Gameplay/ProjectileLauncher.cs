using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO AL : ADD reset fire rate
// ADD cooldown

public class ProjectileLauncher : MonoBehaviour
{
	[SerializeField]
	private Transform _instancePosition = null;

	[SerializeField]
	private Projectile _projectilePrefab = null;

	[SerializeField]
	private bool _canFire = true;

	[SerializeField]
	private int _projectileCount = 1;

	[SerializeField]
	private float _horizontalProjectilesSpacing = 1f;

	[SerializeField]
	private float _fireRate = 0.1f;

	[System.NonSerialized]
	private float _currentFireTime = 0;

	public ProjectileLauncher SetCanFire(bool canFire)
	{
		_canFire = canFire;
		return this;
	}

	public void LaunchProjectile()
	{
		LaunchProjectile(_instancePosition);	
	}

	public void LaunchProjectile(Transform positionTransform)
	{
		LaunchProjectile(positionTransform, _instancePosition.rotation);
	}

	public void LaunchProjectile(Quaternion rotation)
	{
		LaunchProjectile(_instancePosition, rotation);
	}

	public void LaunchProjectile(Transform positionTransform, Quaternion rotation)
	{
		if (_canFire == true && _currentFireTime <= 0)
		{
			DoLaunch(positionTransform, rotation);
		}
	}

	private void Update()
	{
		if (_currentFireTime > 0)
		{
			_currentFireTime -= Time.deltaTime;
		}
	}

	private void DoLaunch(Transform positionTransform, Quaternion rotation)
	{
		float distance = (_horizontalProjectilesSpacing * (_projectileCount - 1));
		Vector3 localPosition = positionTransform.localPosition;
		localPosition.x -= distance * 0.5f;
		for (int i = 0; i < _projectileCount; i++)
		{
			Vector3 firePosition = new Vector3(localPosition.x + _horizontalProjectilesSpacing * i, 0, 0);
			firePosition = positionTransform.position + positionTransform.transform.TransformDirection(firePosition);
			InstantiateProjectile(firePosition, rotation);
		}
	}

	private void InstantiateProjectile(Vector3 position, Quaternion rotation)
	{
		Projectile instance = Instantiate(_projectilePrefab);
		instance.transform.position = position;
		instance.transform.rotation = rotation;
	}

}
