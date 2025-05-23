using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour, InteractableObject
{
    public string ItemName = "";
    Animator animator;

    bool playerInRange = false;
    Rigidbody rb;
    GameObject player;
    public float turnSpeed = 2f;
    public float moveSpeed = 3.5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        animator.SetBool("Sit", true);

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }



    void Update()
    {
        AlignToGround();
        
        if (playerInRange && player != null)
        {
            // Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;
            // Quaternion targetRotation = Quaternion.LookRotation(forward, hit.normal);
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            }

            Vector3 moveDirection = direction.normalized;

            if (moveDirection != Vector3.zero)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run Forward", true);
            }
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

        }
    }

    void AlignToGround()
    {
        RaycastHit hit;

        // Cast down from slightly above the bear to detect the ground
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f))
        {
            // Calculate the forward direction projected onto the slope
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;

            // Calculate the rotation that faces forward along the slope
            Quaternion targetRotation = Quaternion.LookRotation(forward, hit.normal);

            // Smoothly rotate the bear to match the slope
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered bear's area — becoming aggressive!");
            animator.SetBool("Sit", false);
            animator.SetBool("Idle", true);
            playerInRange = true;

            // animator.SetTrigger("Aggressive"); // You can define an "Aggressive" trigger in your Animator
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left bear's area — calming down.");
            if (animator.GetBool("Run Forward"))
            {
                animator.SetBool("Run Forward", false);
            }
            animator.SetBool("Idle", false);
            animator.SetBool("Sit", true);
            playerInRange = false;
        }
    }

    public void HandleClick()
    {

    }

    public string GetItemName()
    {
        return ItemName;
    }
}