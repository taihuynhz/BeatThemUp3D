using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    protected static Audio instance;
    public static Audio Instance => instance;

    [SerializeField] protected AudioSource audioSource;
    public AudioSource AudioSource => audioSource;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
