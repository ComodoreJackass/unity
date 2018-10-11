using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("entered");
        if (other.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            animator.SetBool("close", false);
            animator.SetBool("open", true);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if (other.tag == "Player")
        {
            animator.SetBool("close", false);
            animator.SetBool("open", true);
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exited");
        if (other.tag == "Player")
        {
            animator.SetBool("open", false);
            animator.SetBool("close", true);
        }
    }
}
