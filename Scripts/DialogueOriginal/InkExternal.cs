using Ink.Runtime;

public class InkExternal
{
    private const string Trigger = "trigger";

    public void Bind(Story story, DialogueTriggerManager dialogueTriggerManager)
    {
        story.BindExternalFunction(Trigger, (int id) => dialogueTriggerManager.TriggerScript(id));
    } 

    public void UnBind(Story story)
    {
        story.UnbindExternalFunction(Trigger);
    }
}
