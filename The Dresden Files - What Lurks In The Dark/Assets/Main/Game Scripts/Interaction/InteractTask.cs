using UnityEngine;

public class InteractTask : CustomInteractable
{
    [SerializeField] private string taskSummary;
    [SerializeField] private bool required;
    
    // any effect that should go away once interacted with should be parented under this object.
    [SerializeField] private VoiceLineId voiceLineOnInteract;
    [SerializeField] private GameObject notInteractedEffect;
    
    private int taskId;
    private ObjectiveTracking tracker;
    
    //public override string GetPrompt(HoldableItem item) => "Interact";

    protected override void Start()
    {
        base.Start();
        tracker = ObjectiveTracking.instance;
        if (required) taskId = tracker.AddTask(taskSummary);
        else taskId = tracker.AddOptional();
    }
    
    protected virtual bool destroyScriptOnInteract() => true;
    
    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        bool res = base.Interact(player, heldItem); // do the main interaction trigger / effect
        if (!res) return false;
        // tell the objective tracker that we're done
        if (required) tracker.CompleteTask(taskId);
        else tracker.CompleteOptional(taskId);
        // toggle effects
        if(notInteractedEffect)
            notInteractedEffect.SetActive(false);
        if(voiceLineOnInteract != VoiceLineId.None)
            VoicePlayer.instance.PlayVoiceLine(voiceLineOnInteract);
        // we don't need to keep this as a do-able task; destroy this *component* (not the object)
        if(destroyScriptOnInteract()) Destroy(this);
        return true;
    }
}
