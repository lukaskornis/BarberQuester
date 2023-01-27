using System;
using Unity.XR.CoreUtils;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Trimmer : Tool
{
    public float useShakeStrength = 0.1f;
    public float trimShakeStrength = 0.5f;
    public HairBrush brush;
    
    public AudioClip useSound;
    public AudioClip trimSound;
    public ParticleSystem particles;
    public LayerMask hairLayer;
    public bool isShaving;
    
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        brush = GetComponentInChildren<HairBrush>();
    }


    public override void Use()
    {
        _audioSource.clip = useSound;
        _audioSource.Play();
    }


    public override void EndUse()
    {
        _audioSource.Stop();
    }


    private void OnTriggerStay(Collider other)
    {
        if (!InUse) return;
        // return if layer is not hair
        if (!hairLayer.Contains(other.gameObject.layer)) return;
        print("Trimming");

        if (!isShaving)
        {
            isShaving = true;
            StartShave();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        isShaving = false;
        EndShave();
    }


    void StartShave()
    {
        _audioSource.clip = trimSound;
        particles.Play();
    }


    void EndShave()
    {
        particles.Stop();
    }


    private void Update()
    {
        if (!InUse) return;

        brush.Use();
        ShakeHolder(isShaving ? trimShakeStrength : useShakeStrength);
    }
}