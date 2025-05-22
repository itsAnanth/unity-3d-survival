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

    public void HandleClick() {

        IObjectAction obj = gameObject.GetComponent<IObjectAction>();

        obj.OnClick();
    }
}