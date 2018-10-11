using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCollider : MonoBehaviour {

    private MeshCollider colider;

    // Use this for initialization
    void Start () {
        colider = GetComponent<MeshCollider>();
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
