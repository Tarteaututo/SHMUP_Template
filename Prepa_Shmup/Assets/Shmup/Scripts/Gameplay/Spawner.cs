using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public void SetActor(Actor prefab)
	{
		_actorPrefab = prefab;
	}

	public void SetSpawnRate(float spawnRate)
	{
		_spawnRate = spawnRate;
	}

	public void SetMoveSpeed(float speed)
	{
		_speed = speed;
	}

	public void SetIsReverse(bool isReverse)
	{
		_isReverse = isReverse;
	}
	
	public void SetCanFire(bool canFire)
	{
		_canFire = canFire;
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

		Fireable fireable = instance.GetComponent<Fireable>();
		fireable.SetCanFire(_canFire);
	}
}
