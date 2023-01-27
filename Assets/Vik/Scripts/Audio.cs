using UnityEngine;

public class Audio : MonoBehaviour
{
    static AudioSource source;
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        source = null;
    }


    public static void Play( AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio.Play: clip is null");
            return;
        }
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
    
    public static void Play(AudioClip clip, float volume = 1.0f,float pitch = 1.0f)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio.Play: clip is null");
            return;
        }
        
        source.pitch = pitch;
        source.PlayOneShot(clip, volume);
    }
}
