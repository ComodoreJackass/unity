using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float setSpeed = 0.6f;
    public int degRotation = 90;

    private bool canRotate = true;

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
        callback(true);

    }

    void Update()
    {
        if (Input.GetButtonDown("Right") && canRotate)
        {
            canRotate = false;
            GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(RotateMe(Vector3.up * degRotation, setSpeed, returnValue =>
            { canRotate = returnValue; }));
        }
        if (Input.GetButtonDown("Left") && canRotate)
        {
            canRotate = false;
            GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(RotateMe(Vector3.up * -degRotation, setSpeed, returnValue => 
            { canRotate = returnValue; }));
            
        }
        if (canRotate) {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
