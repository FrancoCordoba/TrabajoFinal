using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
   
    public Transform target;
    public float smoothSpeed = 0.150f;
    [SerializeField]private Vector3 offset;

   


    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
