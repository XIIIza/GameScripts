using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _sceneNumber;

    private void OnEnable()
    {
        int playerPrefs = _player.GetData();
        PlayerPrefs.SetInt("health", playerPrefs);
        SceneManager.LoadScene(_sceneNumber);
    }
}