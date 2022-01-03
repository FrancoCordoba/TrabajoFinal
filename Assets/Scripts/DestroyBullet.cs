using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timerToDestroy = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerToDestroy > 0)
        {
            timerToDestroy -= Time.deltaTime;
            if (timerToDestroy<=0)
            {
                Destroy(gameObject);
            }
        }
    }
}
