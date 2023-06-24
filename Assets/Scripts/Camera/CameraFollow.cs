using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] protected Transform target;

    protected void Update()
    {
        Follow();
    }

    protected void Follow()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
