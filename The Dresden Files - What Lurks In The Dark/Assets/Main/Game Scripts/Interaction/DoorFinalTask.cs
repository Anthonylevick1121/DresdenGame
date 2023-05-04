public class DoorFinalTask : Interactable
{
    public override string GetPrompt(HoldableItem heldItem)
    {
        return ObjectiveTracking.instance.CheckWin() ? "Follow Dresden's Advice, and Escape the Dream" : "";
        // return "Follow Dresden's Advice, and Escape the Dream";
    }
    
    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        if (!ObjectiveTracking.instance.CheckWin())
            return false;
        ObjectiveTracking.instance.AdvanceLevel();
        return true;
    }
}
