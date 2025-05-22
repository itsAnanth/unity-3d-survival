using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public interface InteractableObject
{
    public string GetItemName();

    public void HandleClick();
}