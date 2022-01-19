using System.Collections;
using UnityEngine;

///
/// Decommentez tout ce qui est en commentaire si vous voulez personnaliser des méthodes de spawn.
/// En exemple, une méthode qui ajoute un second timer qui permet de personnaliser un peu plus le spawn.
/// 


//public enum SpawnType
//{
//	Default = 0,
//	Alternative01
//}

[System.Serializable]
public class SpawnerBinder
{
	[SerializeField]
	private SpawnerSettings _spawnerSettings = null;

	/// <summary>
	/// Matched with the WaveManager.Spawner list indexes.
	/// </summary>
	[Range(0, 20)]
	[SerializeField]
	private int _spawnerID = 0;

	//[SerializeField]
	//private SpawnType _spawnType = SpawnType.Default;

	[SerializeField]
	private float _spawnRate = 1f;

	[SerializeField]
	private float _spawnDuration = 5f;

	//[SerializeField]
	//private float _secondSpawnDuration = 5f;

	[SerializeField]
	private float _waitBetweenSpawns = 3f;

	[System.NonSerialized]
	private Spawner _spawner = null;

	public void ResolveSpawner(WaveManager waveManager)
	{
		if (_spawnerID >= waveManager.Spawners.Count)
		{
			Debug.LogErrorFormat("{0} invalid id ({1})", GetType().Name, _spawnerID);
			return;
		}
		_spawner = waveManager.Spawners[_spawnerID];
	}

	public IEnumerator Run()
	{
		_spawnerSettings.Apply(_spawner, _spawnRate);

		yield return RunMethod01();


		//// Commentez CETTE ligne et décommentez le reste si vous voulez utilisez plusieurs méthode de spawn.
		////yield return RunMethod01();

		////switch between methods
		//switch (_spawnType)
		//{
		//	case SpawnType.Default:
		//	default:
		//	{
		//		yield return RunMethod01();
		//	}
		//	break;
		//	case SpawnType.Alternative01:
		//	{
		//		yield return RunMethod02();
		//	}
		//	break;
		//}
	}

	public void Stop()
	{
		_spawner.StopSpawner();
	}

	private IEnumerator RunMethod01()
	{
		_spawner.StartSpawner();

		yield return new WaitForSeconds(_spawnDuration);
		_spawner.StopSpawner();

		yield return new WaitForSeconds(_waitBetweenSpawns);
		yield return RunMethod01();
	}

	//private IEnumerator RunMethod02()
	//{
	//	_spawner.StartSpawner();

	//	yield return new WaitForSeconds(_spawnDuration);
	//	_spawner.StopSpawner();

	//	yield return new WaitForSeconds(_waitBetweenSpawns);
	//	_spawner.StartSpawner();

	//	yield return new WaitForSeconds(_secondSpawnDuration);
	//	_spawner.StopSpawner();

	//	yield return new WaitForSeconds(_waitBetweenSpawns);
	//	yield return RunMethod02();
	//}
}
