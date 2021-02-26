using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Not seen in classroom 
/// TODO AL : comment
/// </summary>
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

	[SerializeField]
	private bool _isDestroyedByCollision = true;

	public void Apply(Spawner spawner)
	{
		// TODO AL : comment about the Builder patterns
		spawner.SetActor(_actorPrefab)
				.SetMoveSpeed(_actorMoveSpeed)
				.SetIsReverse(isReverse)
				.SetSpawnRate(_spawnRate)
				.SetCanFire(_canFire)
				.SetIsDestroyedByCollision(_isDestroyedByCollision);
	}
}
