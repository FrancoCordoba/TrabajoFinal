using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] private GameObject originPointOne;
   // [SerializeField] private float distanceRay = 10f;
    [SerializeField] float minimumDistance;
    [SerializeField] Transform[] waypoints;
    private int currentIndex = 0;
    private bool goBack = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Patroll();
    }
    

    public void Patroll()

    {
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;

        float distance = deltaVector.magnitude;



        if (distance < minimumDistance)
        {
            if (currentIndex >= waypoints.Length - 1)
            {
                goBack = true;
            }
            else if (currentIndex <= 0)
            {
                goBack = false;
            }

            if (!goBack)
            {
                currentIndex++;
            }
            else currentIndex--;
        }
    }
   
    
}

