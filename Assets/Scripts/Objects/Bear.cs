using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour, InteractableObject
{
    public string ItemName = "bear";
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Sit", true);
    }

    public void HandleClick()
    {

    }

    public string GetItemName()
    {
        return ItemName;
    }
}