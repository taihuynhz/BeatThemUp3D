using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointer : MonoBehaviour
{
    protected void OnEnable()
    {
        // Play HandPointer sound
        OnUpdateAudio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/HandPointer") as AudioClip);

        // Hide HandPointer after time
        StartCoroutine(HandPointFlickering());
    }

    protected IEnumerator HandPointFlickering()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
