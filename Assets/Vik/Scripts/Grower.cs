using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Grower : Tool
{
    public float growInterval = 0.5f;
    public ParticleSystem particles;
    public HairBrush brush;

    AudioSource _audioSource;
    float _nextGrowTime;
    
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    private void Update()
    {
        if (InUse)
        {
            if (Time.time > _nextGrowTime)
            {
                _nextGrowTime = Time.time + growInterval;
                Grow();
            }
        }
    }

    
    public override void Use()
    {
        Grow();
    }

    
    void Grow()
    {
        _audioSource.Play();
        brush.Use();
        particles.Play();
    }
}