using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public void ActivateTrigger()
    {
        enabled = true;
    }
}
