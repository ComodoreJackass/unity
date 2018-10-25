﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiedChestScript : MonoBehaviour {

    private Animator animator;
    private new AudioSource audio;
    private bool started = false;
    //s ovom forom skrinje i vrata ce se otvarati samo ako ih direktno gledamo
    private float yRotation;
    private float yRotationPlayer;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();
        yRotation = transform.rotation.eulerAngles.y + 90;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chest in range.");
    }

    private void OnTriggerStay(Collider other)
    {
        yRotationPlayer = other.transform.GetChild(0).rotation.eulerAngles.y;
        if (other.tag == "Player" && yRotation == yRotationPlayer && !started)
        {
            animator.SetBool("openChest", true);
            audio.Play(0);
            started = true;
            Debug.Log("Alexa this is so sad...");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Chest out of range.");
    }
}