using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpToLocation : MonoBehaviour
{
    [SerializeField] private SceneLoader _loadScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _loadScene.enabled = true;
    }
}
