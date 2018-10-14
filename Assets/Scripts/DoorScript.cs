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


    private Animator animator;
    private bool canClose = true;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();;
    }

    public void OpenDoor() {
        animator.SetBool("close", false);
        animator.SetBool("open", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        canClose = false;
    }

    private void OnTriggerExit(Collider other)
    {
        canClose = true;
    }

    public void CloseDoor()
    {
        if (canClose)
        {
            animator.SetBool("open", false);
            animator.SetBool("close", true);
        }
    }
}
