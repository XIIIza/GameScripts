using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
 
public class DialogueManager : MonoBehaviour 
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _displayNameText;
    [SerializeField] private Animator _portraitAnimator; 

    [Header("Load Global JSON")]
    [SerializeField] private TextAsset _loadGlobalJSON;

    [Header("Choices UI")]
    [SerializeField] private DialogueTriggerManager _dialogueTriggerManager;
    [SerializeField] private GameObject[] _choices;

    public bool IsDialoguePlay { get; private set; }

    private static DialogueManager _instance;
    private TextMeshProUGUI[] _choicesText;
    private Story _currentStory;
    private DialogueVaribles _dialogueVaribles;
    private InkExternal _inkExternal;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";



    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        _instance = this;

        _dialogueVaribles = new DialogueVaribles(_loadGlobalJSON);
        _inkExternal = new InkExternal();
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        IsDialoguePlay = false;
        dialoguePanel.SetActive(false);

        _choicesText = new TextMeshProUGUI[_choices.Length];
        int index = 0;
        foreach (GameObject choice in _choices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!IsDialoguePlay)
        {
            return;
        }

        if (_currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, DialogueTriggerManager _dialogueTriggerManager)
    {
        _currentStory = new Story(inkJSON.text);
        IsDialoguePlay = true;
        dialoguePanel.SetActive(true);

        _dialogueVaribles.StartListening(_currentStory);
        _inkExternal.Bind(_currentStory, _dialogueTriggerManager);

        ContinueStory();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        _dialogueVaribles.Variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }


    public void ExitDialogueMode()
    {
        _dialogueVaribles.StopListening(_currentStory);
        _inkExternal.UnBind(_currentStory);
        _dialogueVaribles.SaveVariables();

        IsDialoguePlay = false;
        dialoguePanel.SetActive(false);
        _dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            _dialogueText.text = _currentStory.Continue();
            DisplayChoices();

            HandleTags(_currentStory.currentTags);
        }

        else
        {
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2)
                Debug.LogError("Tag could not be appropriately parsed: " + tag);

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    _displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    _portraitAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;

        if (currentChoices.Count > _choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            _choices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < _choices.Length; i++)
        {
            _choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        InputManager.GetInstance().GetSubmitPressed();
        ContinueStory();
    }

    private void OnApplicationQuit()
    {
        _dialogueVaribles.SaveVariables();
    }
}