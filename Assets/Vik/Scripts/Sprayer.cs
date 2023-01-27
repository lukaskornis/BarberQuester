using UnityEngine;

public class Sprayer : Tool
{
    AudioSource _audioSource;
    public ParticleSystem particles;
    public HairBrush brush;
    public Color color;
    
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        brush = GetComponentInChildren<HairBrush>();

        brush.color = color;
        particles.startColor = new Color( color.r, color.g, color.b, 0.5f );
        particles.Stop();
    }


    public override void Use()
    {
        particles.Play();
        _audioSource.Play();
    }
    
    
    public override void EndUse()
    {
        particles.Stop();
        _audioSource.Stop();
    }

    
    private void Update()
    {
        if (InUse)
        {
            ShakeHolder(0.1f);
            brush.Use();
        }
    }
}