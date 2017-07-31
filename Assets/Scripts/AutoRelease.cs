using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoRelease : MonoBehaviour {

    public float duration;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        float animatorLenght = animator ? animator.GetCurrentAnimatorClipInfo(0)[0].clip.length : 0;

        AudioSource audio = GetComponent<AudioSource>();
        float audioLenght = audio ? audio.clip.length : 0;

        if(duration == 0)
            duration = Mathf.Max(animatorLenght, audioLenght);
    }

    private void OnEnable()
    {
        StartCoroutine(Release());
    }

    private IEnumerator Release ()
    {
        yield return new WaitForSeconds(duration);
        ObjectPool.Release(gameObject);
    }
}
