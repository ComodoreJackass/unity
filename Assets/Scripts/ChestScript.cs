using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

    public Item item;
    private Animator animator;

    private float yRotation;
    private float yRotationPlayer;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        yRotation = transform.rotation.eulerAngles.y+90;
        if (yRotation == 360f) {
            yRotation = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chest in range.");
    }

    private void OnTriggerStay(Collider other)
    {
        yRotationPlayer = other.transform.GetChild(0).rotation.eulerAngles.y;
        Debug.Log(yRotation + " " + yRotationPlayer);
        if (other.tag == "Player" && yRotation == yRotationPlayer)
        {
            animator.SetBool("openChest", true);
            Inventory.instance.Add(item);
            Debug.Log("Picked up " + item.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Chest out of range.");
    }

}
