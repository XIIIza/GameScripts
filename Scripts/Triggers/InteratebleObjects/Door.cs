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
            VisualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                _sceneLoader.enabled = true;
            }
        }

        else
            VisualCue.SetActive(false);
    }
}
