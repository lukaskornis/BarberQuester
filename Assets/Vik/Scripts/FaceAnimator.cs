using System.Collections;
using UnityEngine;

public class FaceAnimator : MonoBehaviour
{
    public Vector2 blinkInterval = new Vector2(1, 3);
    public float blinkDuration = 0.1f;
    
    public GameObject eyes;
    public GameObject mouth;
    

    void Start()
    {
        StartCoroutine(BlinkRoutine());
    }
    
    
    IEnumerator BlinkRoutine()
    {
        var blinkTimer =  new WaitForSeconds(blinkDuration);
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(blinkInterval.x, blinkInterval.y));
            eyes.SetActive(false);
            yield return blinkTimer;
            eyes.SetActive(true);
        }
    }
}
