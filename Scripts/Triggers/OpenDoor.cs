using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Trigger
{
    [SerializeField] private Door _door;

    private void OnEnable()
    {
        _door.SetInteracteble();
    }
}
