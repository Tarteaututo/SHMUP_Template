using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField]
	private Path _path = null;

	[System.NonSerialized]
	private ActorSettings _actorSettings = null;

	[System.NonSerialized]
	private ProjectileLauncherSettings _projectileLauncherSettings = null;
	
	[System.NonSerialized]
	private CollisionDamageSettings _collisionDamageSettings = null;

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
	private ProjectileLauncherBehaviour _projectileLauncherBehaviour = 0;

	public Spawner SetActorSettings(ActorSettings actorSettings)
	{
		_actorSettings = actorSettings;
		return this;
	}
	
	public Spawner SetProjectileLauncherSettings(ProjectileLauncherSettings projectileLauncherSettings)
	{
		_projectileLauncherSettings = projectileLauncherSettings;
		return this;
	}
	
	public Spawner SetCollisionDamageSettings(CollisionDamageSettings collisionDamageSettings)
	{
		_collisionDamageSettings = collisionDamageSettings;
		return this;
	}

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
	
	public Spawner SetProjectileLauncherBehaviour(ProjectileLauncherBehaviour projectileLauncherBehaviour)
	{
		_projectileLauncherBehaviour = projectileLauncherBehaviour;
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
				//SpawnActor();
				SpawnActor_Refacto();
				_currentSpawnTime = _spawnRate;
			}
		}
	}

	private void SpawnActor_Refacto()
	{
		var instance = _actorSettings.InstantiateActor(_path);

		// Cache all getcomponents into a facade class ?
		_projectileLauncherSettings.ApplyTo(instance.GetComponent<ProjectileLauncher>());
		_collisionDamageSettings.ApplyTo(instance.GetComponent<CollisionDamageEmitter>(), instance.GetComponent<CollisionDamageReceiver>());
	}

	private void SpawnActor()
	{
		Actor instance = Instantiate(_actorPrefab);
		instance.SetPath(_path, _isReverse);
		instance.SetMoveSpeed(_speed);

		ProjectileLauncher fireable = instance.GetComponent<ProjectileLauncher>();
		fireable.SetCanFire(_canFire);
		fireable.SetProjectileLauncherBehaviour(_projectileLauncherBehaviour);

		CollisionDamageEmitter collisionDamageEmitter = instance.GetComponent<CollisionDamageEmitter>();
		if (collisionDamageEmitter != null)
		{
			// TODO AL : set life ?
		}
	}
}
