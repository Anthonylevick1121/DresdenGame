using UnityEngine;

public class DoorFinalTask : Interactable
{
    public override string GetPrompt(HoldableItem heldItem)
    {
        //return ObjectiveTracking.instance.CanSleep() ? "Follow Dresden's Advice, and Escape the Dream" : "";
        return "Follow Dresden's Advice, and Escape the Dream";
    }
    
    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        //if (!ObjectiveTracking.instance.CanSleep()) return false;
        // change this to the appropriate win-scene that isn't the lose scene
        ScreenFade.instance.LoadSceneWithFade("EndScene", false);
        return true;
    }
}
