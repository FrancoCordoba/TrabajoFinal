using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0.5f,0,Space.World);
    }
    //GameManager.instance.addScore();
    //Debug.Log(GameManager.instance.getScore());
}
