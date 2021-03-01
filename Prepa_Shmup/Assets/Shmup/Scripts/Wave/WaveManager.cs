using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Brain of the wave system. It hold a list of Waves and a list of Spawners, feeds the spawners to the wave and sequentially execute instructions found in waves.
/// </summary>
public class WaveManager : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private float _delayBetweenWaves = 10f;

	/// <summary>
	/// Endless mode disable the win condition and go back to the first element of wave's list when the index reach the end.
	/// </summary>
	[SerializeField]
	private bool _endless = false;

	/// <summary>
	/// List of wave executed in this scene. Order is important, since all waves will be executed sequentially in the same order.
	/// </summary>
	[SerializeField]
	private List<Wave> _waves = null;

	[ListDrawerSettings(ShowIndexLabels = true)]
	[SerializeField]
	private List<Spawner> _spawners = null;
	
	[HideInEditorMode, ShowInInspector, ReadOnly, InlineEditor]
	[System.NonSerialized]
	private List<Wave> _runtimeWaves = null;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private int _currentWaveIndex = 0;

	[HideInEditorMode, ShowInInspector, ReadOnly]
	[System.NonSerialized]
	private float _currentWaveTime = -1f;

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
	/// <summary>
	/// List of spawners. Order is important, since the index is matched with SpawnerBinder.SpawnerID
	/// </summary>
	public List<Spawner> Spawners => _spawners;

	/// <summary>
	/// Remaining time the wave will last.
	/// </summary>
	public float CurrentWaveTime => _currentWaveTime;

	public int CurrentWaveIndex => _currentWaveIndex;

	/// <summary>
	/// Delay between each wave where every spawners is disabled.
	/// </summary>
	public float CurrentDelayBetweenWaves => _currentDelayBetweenWaves;
	#endregion Properties

	#region Methods
	private void Awake()
	{
		_runtimeWaves = new List<Wave>(_waves.Count);
		for (int i = 0; i < _waves.Count; i++)
		{
			_runtimeWaves.Add(Instantiate(_waves[i]));
		}
	}

	/// <summary>
	/// Start the first wave at the beginning of the game.
	/// DO NOT handle a restart.
	/// </summary>
	public void StartFirstWave()
	{
		_currentWaveIndex = 0;
		StartWave(_currentWaveIndex);
	}

	/// <summary>
	/// Update the wave instructions. To be called when the game is running.
	/// </summary>
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
					_currentWaveIndex = (int)Mathf.Repeat(_currentWaveIndex + 1, _runtimeWaves.Count);
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
	#endregion Methods

}
