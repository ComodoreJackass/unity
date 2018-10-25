using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

    public Item item;
    private Animator animator;

    private bool entered;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        entered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Press 'E' to open chest.");
        entered = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Interact") && entered)
        {
            animator.SetBool("openChest", true);
            Inventory.instance.Add(item);
            Debug.Log("Picked up " + item.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        entered = false;
    }

}
