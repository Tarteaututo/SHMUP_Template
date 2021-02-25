using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLevelSelection : MonoBehaviour
{
    [SerializeField]
    private string level01Name = string.Empty;

    [SerializeField]
    private string level02Name = string.Empty;

    [SerializeField]
    private string level03Name = string.Empty;

    public void LoadLevel01()
    {
        SceneManager.LoadSceneAsync(level01Name);
    }

    public void LoadLevel02()
    {
        SceneManager.LoadSceneAsync(level02Name);
    }

    public void LoadLevel03()
    {
        SceneManager.LoadSceneAsync(level03Name);
    }
}
