using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    
    private Animator _animator;
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Grip = Animator.StringToHash("Grip");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    
    private void Update()
    {
        var trigger = pinchAnimationAction.action.ReadValue<float>();
        _animator.SetFloat( Trigger, trigger);
        
        var grip = gripAnimationAction.action.ReadValue<float>();
        _animator.SetFloat( Grip, grip);
    }
}
