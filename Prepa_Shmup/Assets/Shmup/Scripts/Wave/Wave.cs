using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "GameSup/Wave")]
public class Wave : ScriptableObject
{
	#region Fields
	[SerializeField]
	private List<SpawnSession> _spawnSession = null;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private int _currentSessionIndex = 0;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private float _currentTime = 0f;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private float _currentExitTime = -1f;

	[System.NonSerialized]
	private float _waveDuration = -1f;

	#endregion Fields

	#region Properties
	[ShowInInspector, ReadOnly]
	public float WaveDuration
	{
		get
		{
			if (_waveDuration < 0)
			{
				float duration = 0;
				for (int i = 0; i < _spawnSession.Count; i++)
				{
					var spawnSession = _spawnSession[i];
					duration += spawnSession.Duration + spawnSession.ExitDuration;
				}
				_waveDuration = duration;
			}
			return _waveDuration;
		}
	}
	#endregion Properties

	#region Methods
	public void Run(WaveManager waveManager)
	{
		_spawnSession[0].Run(waveManager);
		_currentExitTime = -1f;
		_currentSessionIndex = 0;
	}

	public void UpdateSession(WaveManager waveManager, float deltaTime)
	{
		_currentTime += deltaTime;
		if (_currentSessionIndex >= _spawnSession.Count)
		{
			return;
		}

		SpawnSession currentSpawnSession = _spawnSession[_currentSessionIndex];

		if (_currentExitTime >= 0)
		{
			_currentExitTime -= deltaTime;

			if (_currentExitTime <= 0)
			{
				Debug.LogFormat("{0} Run session {1}", name, _currentSessionIndex);
				_currentTime = 0;
				_currentSessionIndex++;
				if (_currentSessionIndex < _spawnSession.Count)
				{
					_spawnSession[_currentSessionIndex].Run(waveManager);
				}
			}
		}
		else if (_currentTime > currentSpawnSession.Duration)
		{
			Debug.LogFormat("{0} Stop session {1}", name, _currentSessionIndex);

			_currentExitTime = currentSpawnSession.ExitDuration;
			currentSpawnSession.Stop(waveManager);
		}
	}

	public void Stop(WaveManager waveManager)
	{
		if (_currentSessionIndex >= _spawnSession.Count)
		{
			return;
		}
		SpawnSession currentSpawnSession = _spawnSession[_currentSessionIndex];
		currentSpawnSession.Stop(waveManager);
		_currentExitTime = -1f;
		_currentSessionIndex = 0;
		_currentTime = 0;
	}

	private void OnValidate()
	{
		_waveDuration = -1;
	}
	#endregion Methods

}
