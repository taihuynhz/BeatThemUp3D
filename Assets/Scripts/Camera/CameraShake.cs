using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    protected static CameraShake instance;
    public static CameraShake Instance => instance;

    [SerializeField] protected float shakeDuration = 0.3f;
    [SerializeField] protected float shakeForce = 0.2f;

    protected void Awake() => CreateSingleton();

    public IEnumerator Shake()
    {     
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            Vector3 originalPos = transform.position;
            elapsed += Time.deltaTime;
            transform.position = originalPos + Random.insideUnitSphere * shakeForce;
            yield return null;
        }
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
