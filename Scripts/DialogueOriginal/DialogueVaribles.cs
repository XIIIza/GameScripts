using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVaribles
{
    public Dictionary<string, Ink.Runtime.Object> Variables { get; private set; }

    private Story _globalVariablesStory;
    private const string SaveVariablesKey = "INK_VARIABLES";

    public DialogueVaribles(TextAsset loadGlobalsJSON)
    {
        _globalVariablesStory = new Story(loadGlobalsJSON.text);

        if (PlayerPrefs.HasKey(SaveVariablesKey))
        {
            string jsonState = PlayerPrefs.GetString(SaveVariablesKey);
            _globalVariablesStory.state.LoadJson(jsonState);
        }

        Variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in _globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = _globalVariablesStory.variablesState.GetVariableWithName(name);
            Variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void SaveVariables()
    {
        if (_globalVariablesStory != null)
        {
            VariablesToStory(_globalVariablesStory);
            PlayerPrefs.SetString(SaveVariablesKey, _globalVariablesStory.state.ToJson());
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VaribleChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VaribleChanged;
    }

    private void VaribleChanged(string name, Ink.Runtime.Object value)
    {
        if (Variables.ContainsKey(name))
        {
            Variables.Remove(name);
            Variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in Variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}