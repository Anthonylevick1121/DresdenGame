using UnityEngine;

/// <summary>
/// This is not quite a normal HoldableItem.
/// The hold behavior is entirely different, so it's not even a subclass.
/// It will prevent you from holding something else, though.
/// </summary>
public class InspectInteractTask : InteractTask
{
    // any effect that should go away once interacted with should be parented under this object.
    [SerializeField] private GameObject notInteractedEffect;
    [SerializeField] private VoiceLineId voiceLineOnInteract;
    
    // where should the player camera be positioned/rotated to when inspecting this object?
    [SerializeField] private Transform cameraViewLocation;
    private bool active; // are we inspecting this object?
    private PlayerCore player; // the player we are currently animating
    // below are just cached during inspect
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    
    // how long should it take to animate from the starting position to in front of the player?
    [SerializeField] private float inspectAnimationTime;
    private Vector3 animVelocityCache; // for SmoothDamp of animation
    private float curAnimTime; // how far into the animation are we?
    private bool starting; // are we looking at the object, or going away from the object?
    // not technically needed, but they help with ease of code during animations.
    private Vector3 /*animStartPos, */animEndPos;
    private Quaternion animStartRot, animEndRot;
    
    // taken care of by hiding the hud canvas with ToggleGameInput
    /*public override string GetPrompt(HoldableItem heldItem)
    {
        return active ? "" : base.GetPrompt(heldItem);
    }*/
    
    // basic tasks can immediately destroy when interacted with, but this one is more time-based, and should not.
    protected override bool destroyScriptOnInteract() => false;
    
    // an interaction will disable most input but still look for e or esc input
    protected override bool Interact(PlayerCore player, HoldableItem heldItem)
    {
        if (active || !base.Interact(player, heldItem)) return false;
        active = true;
        player.ToggleGameInput(false, false);
        
        // record current camera pos
        this.player = player;
        Transform cameraTransform = player.view.camera.transform;
        originalCameraPosition = cameraTransform.position;
        originalCameraRotation = cameraTransform.rotation;
        // setup animation
        starting = true;
        curAnimTime = 0;
        animVelocityCache = Vector3.zero;
        //animStartPos = originalCameraPosition;
        animStartRot = originalCameraRotation;
        animEndPos = cameraViewLocation.position;
        animEndRot = cameraViewLocation.rotation;
        // toggle effects
        if(notInteractedEffect)
            notInteractedEffect.SetActive(false);
        return true;
    }
    
    private void Update()
    {
        if (!active) return;
        
        if (curAnimTime >= inspectAnimationTime)
        {
            // not animating, check for leave input
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                // start leave animation
                curAnimTime = 0;
                starting = false;
                animVelocityCache = Vector3.zero;
                //animStartPos = cameraViewLocation.position;
                animStartRot = cameraViewLocation.rotation;
                animEndPos = originalCameraPosition;
                animEndRot = originalCameraRotation;
            }
            return;
        }
        
        // run animation
        curAnimTime += Time.deltaTime;
        Transform cameraTransform = player.view.camera.transform;
        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, animEndPos,
            ref animVelocityCache, inspectAnimationTime, float.MaxValue, Time.deltaTime);
        cameraTransform.rotation = Quaternion.Slerp(animStartRot, animEndRot, curAnimTime / inspectAnimationTime);

        if (curAnimTime < inspectAnimationTime) return;
        
        // end the animation
        if(starting)
        { // end entrance anim
            if(voiceLineOnInteract != VoiceLineId.None)
                VoicePlayer.instance.PlayVoiceLine(voiceLineOnInteract);
        }
        else
        { // end exit anim, returning control to the player
            // the below shouldn't be needed, but uncomment if the camera starts to drift from its default position
            // after a number of interacts.
            //cameraTransform.position = originalCameraPosition;
            //cameraTransform.rotation = originalCameraRotation;
            player.ToggleGameInput(true, false);
            player = null;
            active = false;
        }
    }
}
