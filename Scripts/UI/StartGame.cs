using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();
    }
}