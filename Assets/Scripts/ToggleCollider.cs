using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCollider : MonoBehaviour {

    private MeshCollider colider;
    private GameObject player;
    private PlayerMovement movement;

    // Use this for initialization
    void Start () {
        colider = GetComponent<MeshCollider>();

        //onemogući kretanje tokom otvaranja vrata
        player = GameObject.Find("Player");
        movement = player.GetComponent<PlayerMovement>();
    }

    public void stopMoving() {
        movement.freeze = true;
    }

    public void startMoving()
    {
        movement.freeze = false;
    }

    public void toggle()
    {
        if (colider.enabled == true)
        {
            colider.enabled = false;
        }
        else
        {
            colider.enabled = true;
        }
    }
}
