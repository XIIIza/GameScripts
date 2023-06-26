using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneLoader : Trigger
{
    [SerializeField] private int _sceneNumber;

    private void OnEnable()
    {
        SceneManager.LoadScene(_sceneNumber);
    }
}
