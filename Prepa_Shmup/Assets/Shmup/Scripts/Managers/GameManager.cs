using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager _instance = null;
	public static bool HasInstance => _instance != null;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				var instances = FindObjectsOfType<GameManager>();
				if (instances == null || instances.Length == 0)
				{
					Debug.LogError("No instance of GameManager found");
					return null;
				}
				else if (instances.Length > 1)
				{
					Debug.LogError("Multiples instances of GameManager found, there MUST be only one.");
					return null;
				}
				_instance = instances[0];
			}
			return _instance;
		}
	}
	#endregion Singleton

	#region Fields
	[SerializeField]
	private string _replaySceneName = string.Empty;

	[SerializeField]
	private WaveManager _waveManager = null;

	[SerializeField]
	private TimerMenu _timerMenu = null;

	[SerializeField]
	private EndGameMenu _endGameMenu = null;

	[SerializeField]
	private Player _player = null;

	[SerializeField]
	private Tiler _tiler = null;

	[System.NonSerialized]
	private List<Actor> _actors = new List<Actor>();

	[System.NonSerialized]
	private bool _isRunning = false;

	[System.NonSerialized]
	private bool _waitingForEndGame = false;

	[System.NonSerialized]
	private bool _isWin = false;

	[System.NonSerialized]
	private float _timeInSeconds = 0f;

	[System.NonSerialized]
	private int _score = 0;
	#endregion Fields

	#region Methods
	public void StartGame()
	{
		_isRunning = true;
		_waveManager.StartFirstWave();
		if (_tiler != null)
		{
			_tiler.StartTiler();
		}
	}

	public void Pause()
	{
		_isRunning = false;
		Time.timeScale = 0;
	}

	public void Resume()
	{
		_isRunning = true;
		Time.timeScale = 1;
	}

	public void Replay()
	{
		SceneManager.LoadSceneAsync(_replaySceneName, LoadSceneMode.Single);
		Time.timeScale = 1;
	}

	public void AddScore()
	{
		_score++;
		_endGameMenu.UpdateMenu(_score);
	}

	public void AddScore(ScoreGiver scoreGiver)
	{
		if (scoreGiver == null) return;
		_score += scoreGiver.Score;
		_endGameMenu.UpdateMenu(_score);
	}

	public void DoEndGame(bool isWin)
	{
		_isWin = isWin;
		_waitingForEndGame = true;
		if (isWin == false)
		{
			ActivateEndGame();
		}
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif //UNITY_EDITOR
	}

	public void AddActor(Actor actor)
	{
		if (_actors.Contains(actor) == false)
		{
			_actors.Add(actor);
		}
	}

	public void RemoveActor(Actor actor)
	{
		_actors.Remove(actor);
	}

	private void Update()
	{
		if (_isRunning == true)
		{
			_timeInSeconds += Time.deltaTime;
			_timerMenu.UpdateTimer(_timeInSeconds);
			_waveManager.UpdateWaves();
			if (_player != null)
			{
				_player.UpdatePlayer();
			}

			if (_waitingForEndGame == true && _actors.Count == 0)
			{
				ActivateEndGame();
			}
		}
	}

	private void ActivateEndGame()
	{
		_endGameMenu.DoEndGame(_isWin);
		Pause();
	}
	#endregion Methods

}
