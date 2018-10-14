using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastScript : MonoBehaviour
{

    public bool doorInfront = false;
    DoorScript door;

    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            if (hit.collider.tag == "Door")
            {
                door = hit.collider.gameObject.GetComponentInParent<DoorScript>();
                door.OpenDoor();
                doorInfront = true;
            }
            else
            {
                if (doorInfront) {
                    door.CloseDoor();
                    doorInfront = false;
                }
            }
        }
    }
}
