using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    
   
    [SerializeField] private float distanceRay = 5f;
    [SerializeField] private GameObject shootOrigen;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeShoot = 0f;
    [SerializeField] private float shootCooldown = 2f;
  
    private bool isShoot = true;
    private bool isActive = true;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            if (isShoot)
            {
                RaycastCannon();
                
              
            }
            else
            {
                timeShoot += Time.deltaTime;
            }
            if (timeShoot > shootCooldown)
            {
                isShoot = true;
                
            }
        }
    }
   
    private void RaycastCannon()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(shootOrigen.transform.position, shootOrigen.transform.TransformDirection(Vector3.forward), out hit, distanceRay))
        {
           
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Detectado el Raycast");
                isShoot = false;
                timeShoot = 0;
                GameObject b = Instantiate(bulletPrefab, shootOrigen.transform.position, bulletPrefab.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(shootOrigen.transform.TransformDirection(Vector3.forward) * 10f, ForceMode.Impulse);
              
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        if (isShoot && isActive) 
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(shootOrigen.transform.position, shootOrigen.transform.TransformDirection(Vector3.forward) * distanceRay);
        }
    }


    public void SetActiveCannon(bool status)
    {
        isActive = status;
    }
    


}
