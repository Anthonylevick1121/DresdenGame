using JetBrains.Annotations;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //[HideInInspector] protected new AudioSource audio;
    [SerializeField] private AudioClip interactionSound;
    
    protected virtual void Start()
    {
        //audio = GetComponent<AudioSource>();
        // I am constantly forgetting to set this (on children especially), so I'm just going to do so automatically.
        foreach (Transform t in GetComponentsInChildren<Transform>())
            t.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    
    public void BaseInteract(PlayerCore player, [CanBeNull] HoldableItem heldItem)
    {
        if(Interact(player, heldItem) && interactionSound != null)
            player.interaction.interactSoundPlayer.PlayOneShot(interactionSound);
    }
    
    protected abstract bool Interact( PlayerCore player, [CanBeNull] HoldableItem heldItem);
    
    public abstract string GetPrompt([CanBeNull] HoldableItem heldItem);
}
