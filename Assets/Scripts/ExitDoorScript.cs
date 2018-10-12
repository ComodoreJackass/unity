using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(name);

        if (Input.GetButtonDown("Interact"))
        {
            if (SceneManager.GetActiveScene().name == "Floor1")
            {
                SceneManager.LoadScene("Floor2");
            }
            if (SceneManager.GetActiveScene().name == "Floor2")
            {
                SceneManager.LoadScene("Floor3");
            }
            if (SceneManager.GetActiveScene().name == "Floor3")
            {
                SceneManager.LoadScene("Floor4");
            }
        }
    }
}
