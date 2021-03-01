using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WaveSequence
{
	#region Fields
	[SerializeField]
	private float _duration = 0;

	[SerializeField]
	private float _exitDuration = 5f;

	[SerializeField]
	private List<SpawnerBinder> _spawnerBinders = null;

	[System.NonSerialized]
	private List<Coroutine> _routine = null;
	#endregion Fields

	#region Properties
	public float Duration => _duration;
	public float ExitDuration => _exitDuration;
	public float TotalDuration => Duration + ExitDuration;
	#endregion Properties

	#region Methods
	public void Run(WaveManager waveManager)
	{
		ClearRoutines(waveManager);
		_routine = new List<Coroutine>(_spawnerBinders.Count);
		for (int i = 0, length = _spawnerBinders.Count; i < length; i++)
		{
			SpawnerBinder spawnerBinder = _spawnerBinders[i];
			spawnerBinder.ResolveSpawner(waveManager);
			_routine.Add(waveManager.StartCoroutine(spawnerBinder.Run()));
		}
	}

	public void Stop(WaveManager waveManager)
	{
		if (_routine == null)
		{
			return;
		}
		ClearRoutines(waveManager);
		for (int i = 0, length = _spawnerBinders.Count; i < length; i++)
		{
			_spawnerBinders[i].Stop();
		}
	}

	private void ClearRoutines(WaveManager waveManager)
	{
		if (_routine != null)
		{
			for (int i = 0, length = _routine.Count; i < length; i++)
			{
				waveManager.StopCoroutine(_routine[i]);
			}
			_routine = null;
		}
	}
	#endregion Methods

}
