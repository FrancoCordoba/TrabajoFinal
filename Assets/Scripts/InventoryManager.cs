using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Stack inventoryOne;
   

    void Start()
    {
        inventoryOne = new Stack();
    }
    
   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddInventoryOne(GameObject key)
    {
        inventoryOne.Push(key);

    }
    public GameObject GetInventoryOne()
    {
        return inventoryOne.Pop() as GameObject;
    }
}
