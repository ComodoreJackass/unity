using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour {
    
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        string name = this.name;
	}

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(name);
        if (other.tag == "Player" && Input.GetButtonDown("Interact") && name!="ExitDoor")
        {
            animator.SetBool("close", false);
            animator.SetBool("open", true);
        }

        if (name == "ExitDoor" && Input.GetButtonDown("Interact")) {
            if (SceneManager.GetActiveScene().name == "Floor1")
            {
                SceneManager.LoadScene("Floor2");
            }
            if (SceneManager.GetActiveScene().name == "Floor2")
            {
                SceneManager.LoadScene("Floor3");
            }
            if (SceneManager.GetActiveScene().name == "Floor3")
            {
                SceneManager.LoadScene("Floor4");
            }

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
        if (other.tag == "Player" && name != "ExitDoor")
        {
            animator.SetBool("open", false);
            animator.SetBool("close", true);
        }
    }
}
