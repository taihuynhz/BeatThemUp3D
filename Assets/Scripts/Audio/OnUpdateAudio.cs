using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUpdateAudio : MonoBehaviour
{
    protected static OnUpdateAudio instance;
    public static OnUpdateAudio Instance => instance;

    [SerializeField] protected AudioSource audioSource;
    public AudioSource AudioSource => audioSource;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
