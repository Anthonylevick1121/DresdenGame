using UnityEngine;

public class InteractTask : CustomInteractable
{
    [SerializeField] private string taskSummary;
    [SerializeField] private bool required;
    private int taskId;
    private ObjectiveTracking tracker;
    
    //public override string GetPrompt(HoldableItem item) => "Interact";

    protected override void Start()
    {
        base.Start();
        tracker = ObjectiveTracking.instance;
        if (required) taskId = tracker.AddTask(taskSummary);
        else tracker.AddOptional();
    }
    
    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        bool res = base.Interact(player, heldItem); // do the main interaction trigger / effect
        if (!res) return false;
        // tell the objective tracker that we're done
        if (required) tracker.CompleteTask(taskId);
        else tracker.CompleteOptional();
        return true;
    }
}
