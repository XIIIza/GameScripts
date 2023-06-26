using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternal
{
    public void Bind(Story story, DialogueTriggerManager dialogueTriggerManager)
    {
        story.BindExternalFunction("trigger", (int id) => dialogueTriggerManager.TriggerScript(id));
    } 

    public void UnBind(Story story)
    {
        story.UnbindExternalFunction("trigger");
    }
}
