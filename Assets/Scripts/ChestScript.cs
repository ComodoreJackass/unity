using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

    //chest sadrži slot za item
    public Item item;
    //animacija
    private Animator animator;

    private bool entered;
    private bool pickedUp;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        entered = false;
        pickedUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Press 'E' to open chest.");
        entered = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Interact") && entered && !pickedUp)
        {
            animator.SetBool("openChest", true);
            //Rokni item u inventory
            Inventory.instance.Add(item);
            //ubijanje daljnje interakcije sa škrinjom
            pickedUp = true;
            Debug.Log("Picked up " + item.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        entered = false;
    }

}
