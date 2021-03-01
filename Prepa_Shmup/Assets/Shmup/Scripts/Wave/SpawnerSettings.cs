using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileLauncherBehaviour
{
	ProjectileLauncherForward = 0,
	ActorForward,
	Player
}

[System.Serializable]
public class ActorSettings
{
	[SerializeField]
	private Actor _actorPrefab = null;

	[SerializeField]
	private float _actorMoveSpeed = 2f;

	[SerializeField]
	private bool isReverse = false;

	public Actor InstantiateActor(Path atPath)
	{
		Actor instance = GameObject.Instantiate(_actorPrefab);

		if (atPath != null)
		{
			ApplySettings(instance, atPath);
		}

		return instance;
	}

	private void ApplySettings(Actor actor, Path path)
	{
		actor.SetMoveSpeed(_actorMoveSpeed);
		actor.SetPath(path, isReverse);
	}
}

[System.Serializable]
public class ProjectileLauncherSettings
{
	[SerializeField]
	private bool _canFire = false;

	[SerializeField]
	private ProjectileLauncherBehaviour _projectileLauncherBehaviour = ProjectileLauncherBehaviour.ActorForward;

	public void ApplyTo(ProjectileLauncher projectileLauncher)
	{
		if (projectileLauncher != null)
		{
			projectileLauncher.SetCanFire(_canFire);
			projectileLauncher.SetProjectileLauncherBehaviour(_projectileLauncherBehaviour);
		}
	}
}

[System.Serializable]
public class CollisionDamageSettings
{
	[SerializeField]
	private bool _takeDamageOnCollision = false;

	public void ApplyTo(CollisionDamageHandler collisionDamageHandler)
	{
		if (collisionDamageHandler != null)
		{
			collisionDamageHandler.SetTakeDamageOnCollision(_takeDamageOnCollision);
		}
	}
}

/// <summary>
/// Not seen in classroom 
/// TODO AL : comment
/// </summary>
[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "GameSup/SpawnerSettings")]
public class SpawnerSettings : ScriptableObject
{
	[SerializeField]
	private float _spawnRate = 1f;

	[SerializeField]
	private ActorSettings _actorSettings = null;

	[SerializeField]
	private ProjectileLauncherSettings _projectileLauncherSettings = null;

	[SerializeField]
	private CollisionDamageSettings _collisionDamageSettings = null;

	public void Apply(Spawner spawner)
	{
		spawner.SetActorSettings(_actorSettings);
		spawner.SetProjectileLauncherSettings(_projectileLauncherSettings);
		spawner.SetCollisionDamageSettings(_collisionDamageSettings);
	}
}
