using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] Vector3 cameraOffset; 

    protected void Update()
    {
        Follow();
    }

    protected void Follow()
    {
        //transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(target.position.x, cameraOffset.y, cameraOffset.z);
    }
}
