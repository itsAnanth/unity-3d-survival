using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InteractableObject : MonoBehaviour
{
    public string ItemName;
 
    public string GetItemName()
    {
        return ItemName;
    }

    public void PlaceInInventory()
    {
        Debug.Log("placed in inventory");
        Destroy(gameObject);
    }
}