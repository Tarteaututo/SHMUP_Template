using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "GameSup/SpawnerSettings")]
public class SpawnerSettings : ScriptableObject
{
	[SerializeField]
	private Actor _actorPrefab = null;

	[SerializeField]
	private float _actorMoveSpeed = 2f;

	[SerializeField]
	private bool isReverse = false;

	[SerializeField]
	private float _spawnRate = 1f;

	[SerializeField]
	private bool _canFire = false;

	public void Apply(Spawner spawner)
	{
		spawner.SetActor(_actorPrefab);
		spawner.SetMoveSpeed(_actorMoveSpeed);
		spawner.SetIsReverse(isReverse);
		spawner.SetSpawnRate(_spawnRate);
		spawner.SetCanFire(_canFire);
	}
}
