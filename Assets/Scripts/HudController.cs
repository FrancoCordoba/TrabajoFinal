using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HudController : MonoBehaviour
{
    [SerializeField] private Text textLives;

    [SerializeField] private InventoryManager mgInventory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TestButton()
    {
        Debug.Log("Se detecta click");

    }
   
}
