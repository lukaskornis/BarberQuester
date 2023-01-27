using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public abstract class Tool : MonoBehaviour
{
    public bool useOnlyInHand = true;
    
    protected bool InUse;
    protected XRBaseController Holder;

    void Start()
    {
        var grab = GetComponent<XRGrabInteractable>();
        grab.activated.AddListener(OnUse);
        grab.deactivated.AddListener(OnEndUse);
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnDrop);
    }


    private void OnDrop(SelectExitEventArgs args)
    {
        Holder = null;
        if (useOnlyInHand && InUse)
        {
            InUse = false;
            EndUse();
        }
        
        Drop();
    }
    

    private void OnGrab(SelectEnterEventArgs args)
    {
        Holder = args.interactorObject.transform.GetComponent<XRBaseController>();
        ShakeHolder(0.1f);
        Grab();
    }

    
    private void OnEndUse(DeactivateEventArgs args)
    {
        InUse = false;
        EndUse();
    }
    

    private void OnUse(ActivateEventArgs args)
    {
        InUse = true;
        Holder = args.interactorObject.transform.GetComponent<XRBaseController>();
        Use();
    }
    

    public virtual void Use()
    {
        
    }
    
    
    public virtual void EndUse()
    {
        
    }
    
    
    public virtual void Grab()
    {
        
    }
    
    
    public virtual void Drop()
    {
        
    }

    
    public void ShakeHolder(float strength = 0.5f)
    {
        if (Holder == null) return;
        Holder.SendHapticImpulse(strength, 0.02f);
    }
}