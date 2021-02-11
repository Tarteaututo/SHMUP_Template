using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	[SerializeField]
	private string _sceneName = string.Empty;

	private void Awake()
	{
		SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
	}
}
