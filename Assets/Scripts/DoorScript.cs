using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    #region old
    /*private Animator animator;

    private float yRotation;
    private float yRotationOposite;
    private float yRotationPlayer;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        yRotation = transform.rotation.eulerAngles.y;
        yRotationOposite = yRotation + 180f;
        if (yRotation == 360f)
        {
            yRotation = 0f;
        }
        if (yRotationOposite == 360f) {
            yRotationOposite = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GiveMePlayerRotation(other);
        Debug.Log(yRotation + " " + yRotationOposite + " " + yRotationPlayer);
        if (other.tag == "Player" && (yRotation == yRotationPlayer || yRotationOposite == yRotationPlayer))
        {
            animator.SetBool("close", false);
            animator.SetBool("open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exited");
        if (other.tag == "Player")
        {
            animator.SetBool("open", false);
            animator.SetBool("close", true);
        }
    }

    private void GiveMePlayerRotation(Collider other) {
        yRotationPlayer = other.transform.GetChild(0).rotation.eulerAngles.y;

        if (yRotationPlayer > 45f && yRotationPlayer < 135f)
        {
            yRotationPlayer = 90f;
        }
        else if (yRotationPlayer > 135f && yRotationPlayer < 225f)
        {
            yRotationPlayer = 180f;
        }
        else if (yRotationPlayer > 225f && yRotationPlayer < 315f)
        {
            yRotationPlayer = 270f;
        }
        else
        {
            yRotationPlayer = 0f;
        }
    }*/
    #endregion


    public GameObject waypoint1;
    public GameObject waypoint2;

    private Animator animator;
    private bool entered;
    private string direction;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        entered = false;

        if (waypoint1.GetComponent<Waypoint>().left == waypoint2) { direction = "left"; }
        else if (waypoint1.GetComponent<Waypoint>().right == waypoint2) { direction = "right"; }
        else if (waypoint1.GetComponent<Waypoint>().up == waypoint2) { direction = "up"; }
        else { direction = "down"; }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Press 'E' to open door");
        entered = true;

        if (direction == "left") {
            waypoint1.GetComponent<Waypoint>().left = null;
            waypoint2.GetComponent<Waypoint>().right = null;
        }
        else if (direction == "right") {
            waypoint1.GetComponent<Waypoint>().right = null;
            waypoint2.GetComponent<Waypoint>().left = null;
        }
        else if (direction == "up") {
            waypoint1.GetComponent<Waypoint>().up = null;
            waypoint2.GetComponent<Waypoint>().down = null;
        }
        else {
            waypoint1.GetComponent<Waypoint>().down = null;
            waypoint2.GetComponent<Waypoint>().up = null;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Interact") && entered)
        {
            Debug.Log("");
            animator.SetBool("close", false);
            animator.SetBool("open", true);

            if (direction == "left")
            {
                waypoint1.GetComponent<Waypoint>().left = waypoint2;
                waypoint2.GetComponent<Waypoint>().right = waypoint1;
            }
            else if (direction == "right")
            {
                waypoint1.GetComponent<Waypoint>().right = waypoint2;
                waypoint2.GetComponent<Waypoint>().left = waypoint1;
            }
            else if (direction == "up")
            {
                waypoint1.GetComponent<Waypoint>().up = waypoint2;
                waypoint2.GetComponent<Waypoint>().down = waypoint1;
            }
            else
            {
                waypoint1.GetComponent<Waypoint>().down = waypoint2;
                waypoint2.GetComponent<Waypoint>().up = waypoint1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("close", true);
        animator.SetBool("open", false);
        entered = false;
    }
}
