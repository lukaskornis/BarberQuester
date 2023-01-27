using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DropTeleporter : MonoBehaviour
{
    public float floorHeight = 0;
    private Vector3 startPosition;
    private Quaternion startRotation;
    float restTime = 0; 
    public float maxRestTime = 1;
    public Vector3 velocity;
    private Rigidbody rb;
    private bool teleporting;

    [SerializeField]private AudioClip teleportSound;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (teleporting) return;
        if (!(transform.position.y < floorHeight)) return;
        
        if (velocity.magnitude < 0.1f)
        {
            restTime += Time.deltaTime;
            if (restTime > maxRestTime)
            {
                StartCoroutine(Teleport());
                restTime = 0;
            }
        }
        else
        {
            restTime = 0;
        }
    }
    
    
    IEnumerator Teleport()
    {
        teleporting = true;
        yield return transform.DOScale( 0, 0.3f).SetEase(Ease.InExpo).WaitForCompletion();
        
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        
        Audio.Play(teleportSound, transform.position);
        yield return transform.DOScale(Vector3.one, 0.4f)
            .SetEase(Ease.OutElastic)
            .ChangeStartValue(Vector3.zero);
        
        teleporting = false;
    }
    
    
    
}
