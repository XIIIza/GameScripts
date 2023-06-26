using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;
    [SerializeField] private DialogueTriggerManager _dialogueTriggerManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            DialogueManager.GetInstance().EnterDialogueMode(_inkJSON, _dialogueTriggerManager);
            Destroy(gameObject);
        }
    }
}
