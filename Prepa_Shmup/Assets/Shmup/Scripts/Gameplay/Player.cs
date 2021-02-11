using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerFire
{
	[SerializeField]
	private string inputName = "Fire1";

	[SerializeField]
	private Projectile _projectile = null;

	[SerializeField]
	private int _multipleProjectileCount = 3;

	[SerializeField]
	private float _multipleProjectileSpacing = 1f;

	[SerializeField]
	private float _fireRate = 0.1f;

	[SerializeField]
	private Transform _muzzle = null;

	[System.NonSerialized]
	private float _currentFireTime = 0;

	public void HandleFire()
	{
		bool fired = Input.GetButton(inputName);

		if (fired == true && _currentFireTime <= 0)
		{
			_currentFireTime = _fireRate;
			DoFireMultiple();
		}

		if (_currentFireTime > 0)
		{
			_currentFireTime -= Time.deltaTime;
		}
	}

	private void DoFire(Vector3 position)
	{
		var instance = Object.Instantiate(_projectile);
		instance.transform.position = _muzzle.transform.TransformPoint(position);
		instance.transform.rotation = _muzzle.transform.rotation;
	}

	private void DoFireMultiple()
	{
		float distance = (_multipleProjectileSpacing * (_multipleProjectileCount - 1));
		Vector3 position = _muzzle.transform.localPosition;
		position.x -= distance * 0.5f;
		for (int i = 0; i < _multipleProjectileCount; i++)
		{
			Vector3 firePosition = position;
			firePosition.x = position.x + _multipleProjectileSpacing * i;
			DoFire(firePosition);
		}
	}

}

public class Player : MonoBehaviour
{
	//[SerializeField]
	//private Projectile _projectile = null;

	//[SerializeField]
	//private int _multipleProjectileCount = 3;

	//[SerializeField]
	//private float _multipleProjectileSpacing = 1f;

	[SerializeField]
	private PlayerFire _playerFire = null;

	[SerializeField]
	private PlayerFire _altPlayerFire = null;

	[SerializeField]
	private WorldLimits _worldLimits = null;

	[SerializeField]
	private Damageable _damageable = null;

	[SerializeField]
	private float _moveSpeed = 1f;

	//[SerializeField]
	//private float _fireRate = 0.1f;

	//[System.NonSerialized]
	//private float _currentFireTime = 0;

	public void UpdatePlayer()
	{
		Move();
		_playerFire.HandleFire();
		_altPlayerFire.HandleFire();
	}

	private void Move()
	{
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		Vector3 desiredPosition = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * _moveSpeed);
		transform.position = _worldLimits.Clamp(desiredPosition);
	}

	private void OnTriggerEnter(Collider other)
	{
		var actor = other.GetComponentInParent<Actor>();

		if (actor != null)
		{
			_damageable.DoDamage(actor.GetCollisionDamage());
			Damageable actorDamageable = actor.GetComponent<Damageable>();
			if (actorDamageable != null)
			{
				 actorDamageable.DoDamage(1000);
			}
		}
	}
}
