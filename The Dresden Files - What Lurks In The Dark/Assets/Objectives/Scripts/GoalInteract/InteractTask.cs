using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractTask : Interactable
{
    public override string GetPrompt(HoldableItem item) => "Start Laundry";

    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        //Hardcoded to go to next level for now
        //Modify later to check if all other essential objects in scene have been
        //interacted with before moving on
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        return true;
    }
}
