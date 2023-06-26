using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractebleObjects
{
    [SerializeField] private SceneLoader _sceneLoader;

    public void SetInteracteble()
    {
        SpriteRenderer.sprite = SecondStateImage;
        IsInteracteble = true;
    }

    private void FixedUpdate()
    {
        if (IsInteracteble && InRange)
        {
            _visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                _sceneLoader.enabled = true;
            }
        }

        else
            _visualCue.SetActive(false);
    }
}
