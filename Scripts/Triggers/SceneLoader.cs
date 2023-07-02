using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _sceneNumber;

    private string _health = "health";

    private void OnEnable()
    {
        int playerPrefs = _player.GetData();
        PlayerPrefs.SetInt(_health, playerPrefs);
        SceneManager.LoadScene(_sceneNumber);
    }
}