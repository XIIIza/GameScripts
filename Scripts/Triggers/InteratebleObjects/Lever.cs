using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractebleObjects
{
    [SerializeField] private Door _door;

    private void FixedUpdate()
    {
        if (IsInteracteble && InRange)
        {
            VisualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                SpriteRenderer.sprite = SecondStateImage;
                _door.SetInteracteble();
                IsInteracteble = false;
            }
        }

        else
            VisualCue.SetActive(false);
    }
}
