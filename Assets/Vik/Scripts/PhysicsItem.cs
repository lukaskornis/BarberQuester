using UnityEngine;

[RequireComponent( typeof( Rigidbody ))]
public class PhysicsItem : MonoBehaviour
{
    public AudioClip impactSound;
    public GameObject impactParticles;

    private void OnCollisionEnter(Collision collision)
    {
        var velocity = collision.relativeVelocity.magnitude;
        if (velocity < 1) return;
        
        var force = Mathf.InverseLerp(0.1f,6,velocity);
        Audio.Play(impactSound, transform.position, force);

        if (impactParticles != null && force > 0.3f)
        {
            var rot = collision.contacts[0].normal;
            Instantiate(impactParticles, collision.contacts[0].point, Quaternion.LookRotation(rot));
        }
    }
}