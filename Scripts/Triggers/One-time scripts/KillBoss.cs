using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoss : Trigger
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LastSceneMinotaur _lastSceneMinotaur;

    private void OnEnable()
    {
        DialogueManager.GetInstance().ExitDialogueMode();
        _enemy.TakeDamage(100000);
        _lastSceneMinotaur.enabled = true;
    }
}
