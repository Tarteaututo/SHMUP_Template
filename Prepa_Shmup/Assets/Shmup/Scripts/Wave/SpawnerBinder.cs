using System.Collections;
using System.Threading;
using UnityEngine;


// TODO AL : changer waitbetweenspawn mais plutot dire : combien de spawn durant cette durée ?

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

	[SerializeField]
	private float _duration = 5f;

	[SerializeField]
	private float _waitBetweenSpawns = 0f;

	[SerializeField]
	private float _waitBeforeBeginSpawn = 0f;

	[System.NonSerialized]
	private Spawner _spawner = null;

	#region Properties
	public float Duration => _duration;
	#endregion Properties

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
		_spawnerSettings.Apply(_spawner);
		yield return RunMethod01();
	}

	public void Stop()
	{
		_spawner.StopSpawner();
	}

	private IEnumerator RunMethod01()
	{
		yield return new WaitForSeconds(_waitBeforeBeginSpawn);

		_spawner.StartSpawner();
		yield return new WaitForSeconds(_duration);

		_spawner.StopSpawner();
		yield return new WaitForSeconds(_waitBetweenSpawns);

		yield return RunMethod01();
	}

}
