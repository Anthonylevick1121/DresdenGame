public class FinalTask : Interactable
{
    public override string GetPrompt(HoldableItem heldItem)
    {
        return ObjectiveTracking.instance.CanSleep() ? "Rest for the night" : "";
    }
    
    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        if (!ObjectiveTracking.instance.CanSleep()) return false;
        ObjectiveTracking.instance.AdvanceLevel();
        return true;
    }
}
