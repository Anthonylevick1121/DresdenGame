public class BedFinalTask : Interactable
{
    public override string GetPrompt(HoldableItem heldItem)
    {
        // cannot sleep if we satisfy win conditions
        return ObjectiveTracking.instance.CanSleep() ?
            ObjectiveTracking.instance.CheckWin() ? "Your heart races... you must escape." : "Rest for the night" : "";
    }
    
    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        if (!ObjectiveTracking.instance.CanSleep() || ObjectiveTracking.instance.CheckWin()) return false;
        ObjectiveTracking.instance.AdvanceLevel();
        return true;
    }
}
