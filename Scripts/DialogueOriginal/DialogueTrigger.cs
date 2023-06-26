using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject _visualCue;

    [Header("Json")]
    [SerializeField] private TextAsset _inkJSON;
    [SerializeField] private TextAsset _repeatedText;
    [SerializeField] private DialogueTriggerManager _dialogueTriggerManager;

    private bool _playerInRange = false;
    private bool _alreadyTalked = false;

    private void Start()
    {
        _visualCue.SetActive(false);
    }

    private void Update()
    {
        CueShower();
    }

    private void CueShower()
    {
        if (_playerInRange && !DialogueManager.GetInstance().IsDialoguePlay)
        {
            _visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                if (!_alreadyTalked)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(_inkJSON, _dialogueTriggerManager);
                    _alreadyTalked = true;
                }

                else
                    DialogueManager.GetInstance().EnterDialogueMode(_repeatedText, _dialogueTriggerManager);
            }
        }

        else
        {
            _visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _playerInRange = false;
        }
    }
}