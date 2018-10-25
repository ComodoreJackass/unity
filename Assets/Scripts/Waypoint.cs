using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;

    private void OnDrawGizmos()
    {
        if (up != null) {
            Gizmos.color = new Color(255,0,0);
            Gizmos.DrawLine(transform.position, up.transform.position);
        }
        if (down != null)
        {
            Gizmos.color = new Color(255, 0, 0);
            Gizmos.DrawLine(transform.position, down.transform.position);
        }
        if (left != null)
        {
            Gizmos.color = new Color(255, 0, 0);
            Gizmos.DrawLine(transform.position, left.transform.position);
        }
        if (right != null)
        {
            Gizmos.color = new Color(255, 0, 0);
            Gizmos.DrawLine(transform.position, right.transform.position);
        }
    }

}
