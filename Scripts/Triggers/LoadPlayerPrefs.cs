using UnityEngine;

public class LoadPlayerPrefs : MonoBehaviour
{
    [SerializeField] private Player _player;

    private string _health = "health";

    private void Start()
    {
        int prefs = PlayerPrefs.GetInt(_health);

        if (prefs == 0)
            _player.SetData(100);
        else
            _player.SetData(PlayerPrefs.GetInt(_health));

        _player.UpdateStats();
    }
}