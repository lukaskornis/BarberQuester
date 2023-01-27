using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopSequencer : MonoBehaviour
{
    public List<GameObject> objects;
    public float delay = 4;
    public float interval = 1;
    public float duration = 0.5f;
    public AudioClip popSound;
    public bool playOnStart = true;

    private void Start()
    {
        if (playOnStart) Pop();
    }


    public void Pop()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        // at start, all objects are inactive
        foreach (var obj in objects)
        {
            obj.SetActive(false);
            obj.transform.localScale = Vector3.zero;
        }
        
        yield return new WaitForSeconds(delay);
        
        var wait = new WaitForSeconds(interval);
        foreach (var obj in objects)
        {
            obj.SetActive(true);
            obj.transform.DOScale(Vector3.one, duration).ChangeStartValue(Vector3.zero).SetEase(Ease.OutElastic);
            Audio.Play(popSound, obj.transform.position);
            yield return wait;
        }
    }
}