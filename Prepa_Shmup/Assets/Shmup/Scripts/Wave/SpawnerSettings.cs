using System.Collections;
using System.Collections.Generic;
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
	private bool _canFire = false;

	public void Apply(Spawner spawner, float spawnRate)
	{
		spawner.SetActor(_actorPrefab);
		spawner.SetMoveSpeed(_actorMoveSpeed);
		spawner.SetIsReverse(isReverse);
		spawner.SetSpawnRate(spawnRate);
		spawner.SetCanFire(_canFire);
	}
}
