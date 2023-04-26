using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationClipAction : MonoBehaviour
{
    // this is the name of an animation parameter in the attached controller; a parameter of type Trigger.
    public string animationTriggerName;
    
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void PlayClip()
    {
        animator.SetTrigger(animationTriggerName);
    }
}
