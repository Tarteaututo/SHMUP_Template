using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Wave", menuName = "GameSup/Wave")]
public class Wave : ScriptableObject
{
	#region Fields
	[FormerlySerializedAs("_spawnSession")]
	[SerializeField]
	private List<WaveSequence> _waveSequence = null;

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
	[ShowInInspector, ReadOnly, PropertyOrder(-1)]
	public float WaveDuration
	{
		get
		{
			if (_waveDuration < 0)
			{
				float duration = 0;
				for (int i = 0; i < _waveSequence.Count; i++)
				{
					duration += _waveSequence[i].TotalDuration;
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
		_waveSequence[0].Run(waveManager);
		_currentExitTime = -1f;
		_currentSessionIndex = 0;
	}

	public void UpdateSession(WaveManager waveManager, float deltaTime)
	{
		_currentTime += deltaTime;
		if (_currentSessionIndex >= _waveSequence.Count)
		{
			return;
		}

		WaveSequence currentWaveSequence = _waveSequence[_currentSessionIndex];

		if (_currentExitTime >= 0)
		{
			_currentExitTime -= deltaTime;

			if (_currentExitTime <= 0)
			{
				Debug.LogFormat("{0} Run sequence {1}", name, _currentSessionIndex);
				_currentTime = 0;
				_currentSessionIndex++;
				if (_currentSessionIndex < _waveSequence.Count)
				{
					_waveSequence[_currentSessionIndex].Run(waveManager);
				}
			}
		}
		else if (_currentTime > currentWaveSequence.Duration)
		{
			Debug.LogFormat("{0} Stop sequence {1}", name, _currentSessionIndex);

			_currentExitTime = currentWaveSequence.ExitDuration;
			currentWaveSequence.Stop(waveManager);
		}
	}

	public void Stop(WaveManager waveManager)
	{
		if (_currentSessionIndex >= _waveSequence.Count)
		{
			return;
		}
		WaveSequence currentSpawnSession = _waveSequence[_currentSessionIndex];
		currentSpawnSession.Stop(waveManager);
		_currentExitTime = -1f;
		_currentSessionIndex = 0;
		_currentTime = 0;

	}

	private void OnValidate()
	{
		// Reset _waveDuration at any user modification
		_waveDuration = -1;
	}
	#endregion Methods

}
