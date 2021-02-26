using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

// TODO AL : move every actor component to a struct ?

public class Spawner : MonoBehaviour
{
	[SerializeField]
	private Path _path = null;

	[System.NonSerialized]
	private Actor _actorPrefab = null;

	[System.NonSerialized]
	private float _spawnRate = 1f;

	[System.NonSerialized]
	private float _speed = 1f;

	[System.NonSerialized]
	private bool _isReverse = false;

	[System.NonSerialized]
	private bool _isStarted = false;

	[System.NonSerialized]
	private float _currentSpawnTime = 0f;

	[System.NonSerialized]
	private bool _canFire = false;

	[System.NonSerialized]
	private bool _isDestroyedByCollision = false;

	public Spawner SetActor(Actor prefab)
	{
		_actorPrefab = prefab;
		return this;
	}

	public Spawner SetSpawnRate(float spawnRate)
	{
		_spawnRate = spawnRate;
		return this;
	}

	public Spawner SetMoveSpeed(float speed)
	{
		_speed = speed;
		return this;
	}

	public Spawner SetIsReverse(bool isReverse)
	{
		_isReverse = isReverse;
		return this;
	}

	public Spawner SetCanFire(bool canFire)
	{
		_canFire = canFire;
		return this;
	}
	
	public Spawner SetIsDestroyedByCollision(bool isDestroyedByCollision)
	{
		_isDestroyedByCollision = isDestroyedByCollision;
		return this;
	}

	public void StartSpawner()
	{
		_isStarted = true;
	}

	public void StopSpawner()
	{
		_isStarted = false;
	}

	private void Update()
	{
		if (_isStarted == true)
		{
			_currentSpawnTime -= Time.deltaTime;

			if (_currentSpawnTime <= 0)
			{
				SpawnActor();
				_currentSpawnTime = _spawnRate;
			}
		}
	}

	private void SpawnActor()
	{
		Actor instance = Instantiate(_actorPrefab);
		instance.SetPath(_path, _isReverse);
		instance.SetSpeed(_speed);

		ProjectileLauncher fireable = instance.GetComponent<ProjectileLauncher>();
		fireable.SetCanFire(_canFire);

		CollisionDamageEmitter collisionDamageEmitter = instance.GetComponent<CollisionDamageEmitter>();
		if (collisionDamageEmitter != null)
		{
			collisionDamageEmitter.SetIsDestroyedByCollision(_isDestroyedByCollision);
		}
	}
}
