using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeChaser : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject player;
    [SerializeField] private float distance;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float minimumDistance;
    [SerializeField] float rotationSpeed;
    [SerializeField] float speed;
    private bool onSight;
    private int currentIndex = 0;
    private bool goBack = false;

    public NavMeshAgent _agent;
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }
    private void ChasePlayer()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance < 5f)
        {
            onSight = true;

        }
        if (distance > 5f)
        {
            onSight = false;
        }
        if (onSight)
        {
            _agent.isStopped = false;
            _agent.SetDestination(player.transform.position);
        }
        if (!onSight)
        {
            _agent.isStopped = true;
            Patroll();
        }

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
