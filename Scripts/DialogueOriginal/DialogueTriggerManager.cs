using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerManager : MonoBehaviour
{
    [SerializeField] private Trigger[] _triggers;

    public void TriggerScript(int _triggerID)
    {
        _triggers[_triggerID].ActivateTrigger();
    }
}
