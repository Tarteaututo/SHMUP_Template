using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private float _delayBetweenWaves = 10f;

	[SerializeField]
	private bool _endless = false;

	[SerializeField]
	private List<Wave> _waves = null;

	[ListDrawerSettings(ShowIndexLabels = true)]
	[SerializeField]
	private List<Spawner> _spawners = null;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private List<Wave> _runtimeWaves = null;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private float _currentWaveTime = -1f;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private int _currentWaveIndex = 0;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private float _currentDelayBetweenWaves = 0;
	#endregion Fields

	#region Events
	[System.Serializable]
	public class WaveManager_UnityEvent : UnityEvent { }

	public WaveManager_UnityEvent PlayerWin = null;
	#endregion Events

	#region Properties
	public List<Spawner> Spawners => _spawners;
	#endregion Properties


	private void Awake()
	{
		_runtimeWaves = new List<Wave>(_waves.Count);
		for (int i = 0; i < _waves.Count; i++)
		{
			_runtimeWaves.Add(Instantiate(_waves[i]));
		}
	}

	//private void OnEnable()
	//{
	//	StartWave(0);
	//}

	//private void OnDisable()
	//{
	//	Stop();
	//}

	public void StartFirstWave()
	{
		StartWave(0);
	}

	public void UpdateWaves()
	{
		float deltaTime = Time.deltaTime;

		if (_currentWaveTime > 0)
		{
			_runtimeWaves[_currentWaveIndex].UpdateSession(this, deltaTime);

			_currentWaveTime -= deltaTime;
			if (_currentWaveTime < 0)
			{
				_currentDelayBetweenWaves = _delayBetweenWaves;
				_runtimeWaves[_currentWaveIndex].Stop(this);
			}
		}
		else
		{
			_currentDelayBetweenWaves -= deltaTime;
			if (_currentDelayBetweenWaves < 0)
			{
				if (_endless == true)
				{
					_currentWaveIndex = (int)Mathf.Repeat(_currentWaveIndex + 1, _runtimeWaves.Count - 1);
				}
				else
				{
					_currentWaveIndex++;
				}
				if (_currentWaveIndex < _runtimeWaves.Count)
				{
					StartWave(_currentWaveIndex);
				}
				else
				{
					// non endless win condition
					PlayerWin.Invoke();
				}
			}
		}
	}

	private void StartWave(int index)
	{
		Wave wave = _runtimeWaves[index];
		wave.Run(this);
		_currentWaveTime = wave.WaveDuration;
	}
}
