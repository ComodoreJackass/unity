using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Final boss!");
        EncounterController.instance.InitiateFinalBattle();
    }
}
