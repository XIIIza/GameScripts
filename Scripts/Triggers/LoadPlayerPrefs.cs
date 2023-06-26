using UnityEngine;

public class LoadPlayerPrefs : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Start()
    {
        int prefs = PlayerPrefs.GetInt("health");

        if (prefs == 0)
            _player.SetData(100);
        else
            _player.SetData(PlayerPrefs.GetInt("health"));

        _player.UpdateStats();
    }
}