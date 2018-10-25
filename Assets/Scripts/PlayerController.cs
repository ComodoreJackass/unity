using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject currentWaypoint;
    private GameObject targetWaypoint;

    private bool executingMovement;
    private bool executingRotate;
    private float pathTraversed;
    private float rotation;
    private int degRotation = 90;

    public float speed = 2.5f;
    public float inTime = 0.6f;

    private void Start()
    {
        executingRotate = false;
        executingMovement = false;
        rotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        rotation = transform.eulerAngles.y;

        if (Input.GetButtonDown("Forward") && !executingRotate && !executingMovement) {
            targetWaypoint = FindTarget(true);
            pathTraversed = 0f;
            if (targetWaypoint != null) {
                executingMovement = true;
            }
        }

        if (Input.GetButtonDown("Back") && !executingRotate && !executingMovement)
        {
            targetWaypoint = FindTarget(false);
            pathTraversed = 0f;
            if (targetWaypoint != null)
            {
                executingMovement = true;
            }
        }

        if (executingMovement)
        {
            // sumiramo u brzinu, kad dođe do jedan prešli smo cijeli put
            // Time.deltaTime služi da brzina ne zavisi o broju renderiranih frejmova
            pathTraversed += speed * Time.deltaTime;

            // Lerp je smoothing funkcija kojoj damo start end i inkrement
            // transform.position je pozicija lika
            transform.position = Vector3.Lerp(currentWaypoint.transform.position, targetWaypoint.transform.position, pathTraversed);
            
            // Ako smo se pomakli na destinaciju, omogući unos novog kretanja
            // i omogući rotaciju kamere
            if (transform.position == targetWaypoint.transform.position)
            {
                //Debug.Log(currentWaypoint.name + " " + targetWaypoint.name);
                currentWaypoint = targetWaypoint;
                executingMovement = false;            
            }
        }

        if (Input.GetButtonDown("Right") && !executingRotate && !executingMovement)
        {
            executingRotate = true;
            StartCoroutine(RotateMe(Vector3.up * degRotation, inTime, returnValue =>
            { executingRotate = returnValue; }));
        }

        if (Input.GetButtonDown("Left") && !executingRotate && !executingMovement)
        {
            executingRotate = true;
            StartCoroutine(RotateMe(Vector3.up * -degRotation, inTime, returnValue =>
            { executingRotate = returnValue; }));

        }
    }

    private GameObject FindTarget(bool forward) {
        if (rotation == 90)
        {
            if (forward)
            {
                return currentWaypoint.GetComponent<Waypoint>().left;
            }
            else
            {
                return currentWaypoint.GetComponent<Waypoint>().right;
            }
            
        }
        else if (rotation == 180)
        {
            if (forward)
            {
                return currentWaypoint.GetComponent<Waypoint>().up;
            }
            else
            {
                return currentWaypoint.GetComponent<Waypoint>().down;
            }
        }
        else if (rotation == 270)
        {
            if (forward)
            {
                return currentWaypoint.GetComponent<Waypoint>().right;
            }
            else
            {
                return currentWaypoint.GetComponent<Waypoint>().left;
            }
        }
        else
        {
            if (forward)
            {
                return currentWaypoint.GetComponent<Waypoint>().down;
            }
            else
            {
                return currentWaypoint.GetComponent<Waypoint>().up;
            }
        }
    }


    //korutina https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
    IEnumerator RotateMe(Vector3 byAngles, float inTime, System.Action<bool> callback)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        /* Quaternion.Slerp radi interpolaciju od a do b po vremenu t u intervalu [0,1]
           for petlja nikada neće dati točno 1, pa za završnu rotaciju još ga
           jednom pozivamo i dajemo mu točno 1*/
        transform.rotation = Quaternion.Slerp(fromAngle, toAngle, 1);
        // yield retun null čeka idući frame i onda nastavlja izvršavanje
        yield return null;
        callback(false);

    }
}
